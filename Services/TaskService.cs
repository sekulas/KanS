using AutoMapper;
using KanS.Entities;
using KanS.Exceptions;
using KanS.Interfaces;
using KanS.Models;
using Microsoft.EntityFrameworkCore;

namespace KanS.Services;

public class TaskService : ITaskService {
    private readonly KansDbContext _context;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public TaskService(KansDbContext context, IUserContextService userContextService, IMapper mapper) {
        _context = context;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<int> CreateTask(int boardId, int sectionId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .Include(ub => ub.Board)
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "true");

        if(ub == null) {
            throw new NotFoundException("Cannot add task to non-existing board.");
        }

        var section = await _context.Sections
            .Include(s => s.Tasks)
            .FirstOrDefaultAsync(s => s.Id == sectionId && !s.Deleted && s.BoardId == boardId);

        if(section == null) {
            throw new NotFoundException("Cannot add task to a non-existing section.");
        }

        int nextId = await _context.Tasks.CountAsync() + 1;

        foreach(var existingTask in section.Tasks) {
            existingTask.Position++;
        }

        TaskE task = new TaskE() {
            Id = nextId,
            BoardId = boardId,
            SectionId = sectionId,
            Position = 0,
        };

        await _context.Tasks.AddAsync(task);

        await _context.SaveChangesAsync();

        return nextId;
    }

    public async Task<TaskDto> GetTaskById(int boardId, int taskId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards.AsNoTracking()
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "true");

        if(ub == null) {
            throw new NotFoundException("Cannot get a task - Board not found.");
        }

        var task = await _context.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == taskId && !j.Deleted && j.BoardId == boardId);

        if(task == null) {
            throw new NotFoundException("Task not found.");
        }

        var taskDto = _mapper.Map<TaskDto>(task);

        return taskDto;
    }

    public async Task RemoveTask(int boardId, int taskId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
                    .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "true");
        
        if(ub == null) {
            throw new NotFoundException("Cannot remove a task - Board not found.");
        }

        var task = await _context.Tasks
            .FirstOrDefaultAsync(j => j.Id == taskId && !j.Deleted && j.BoardId == boardId);

        if( task == null) {
            throw new NotFoundException("There is no task like this to remove");
        }

        await _context.Tasks
            .Where(t => t.SectionId == task.SectionId && t.Position > task.Position)
            .ForEachAsync(t => t.Position--);

        task.Deleted = true;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(int boardId, int taskId, TaskUpdateDto taskDto) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
                    .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "true");

        if(ub == null) {
            throw new NotFoundException("Cannot update a task - Board not found.");
        }

        var task = await _context.Tasks
                    .FirstOrDefaultAsync(j => j.Id == taskId && !j.Deleted && j.BoardId == boardId);

        if(task == null) {
            throw new NotFoundException("There is no task like this to update");
        }

        if(taskDto.SectionId != null && taskDto.Position != null) {
            int oldSectionId = task.SectionId;
            int newSectionId = (int) taskDto.SectionId;
            int oldPosition = task.Position;
            int newPosition = (int) taskDto.Position;
            bool didSectionChange = newSectionId != oldSectionId ? true : false;


            if(didSectionChange) {
                await _context.Tasks
                    .Where(t => t.SectionId == newSectionId && t.Position >= newPosition)
                    .ForEachAsync(t => t.Position++);

                await _context.Tasks
                    .Where(t => t.SectionId == oldSectionId && t.Position > oldPosition)
                    .ForEachAsync(t => t.Position--);

                var newSection = await _context.Sections
                    .FirstOrDefaultAsync(s => s.Id == taskDto.SectionId);

                if(newSection != null && newSection.BoardId == boardId) {
                    task.SectionId = (int) taskDto.SectionId;
                }
                else {
                    throw new NotFoundException("Cannot find newly specified section for task.");
                }
            }
            else {

                if(newPosition > oldPosition) {
                    await _context.Tasks
                        .Where(t => t.SectionId == newSectionId && t.Position > oldPosition && t.Position <= newPosition)
                        .ForEachAsync(t => t.Position--);
                }
                else if(newPosition < oldPosition) {
                    await _context.Tasks
                        .Where(t => t.SectionId == newSectionId && t.Position < oldPosition && t.Position >= newPosition)
                        .ForEachAsync(t => t.Position++);
                }
            }

            task.Position = newPosition;
        }
        if(taskDto.Name != null) {
            task.Name = taskDto.Name;
        }
        if(taskDto.AssignedTo != null) {
            task.AssignedTo = taskDto.AssignedTo;
        }

        await _context.SaveChangesAsync();
    }
}
