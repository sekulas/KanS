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
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot add task to non-existing board.");
        }

        var section = await _context.Sections
            .Include(s => s.Tasks)
            .FirstOrDefaultAsync(s => s.Id == sectionId && !s.Deleted && s.BoardId == boardId);

        int position = section.Tasks.Count();

        int nextId = await _context.Tasks.CountAsync() + 1;

        TaskE task = new TaskE() {
            Id = nextId,
            BoardId = boardId,
            SectionId = sectionId,
            Position = position,
        };

        await _context.Tasks.AddAsync(task);

        await _context.SaveChangesAsync();

        return nextId;
    }

    public async Task<TaskDto> GetTaskById(int boardId, int taskId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot get a task - Board not found.");
        }

        var task = await _context.Tasks
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
                    .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);
        
        if(ub == null) {
            throw new NotFoundException("Cannot remove a task - Board not found.");
        }

        var task = await _context.Tasks
            .FirstOrDefaultAsync(j => j.Id == taskId && !j.Deleted && j.BoardId == boardId);

        if( task == null) {
            throw new NotFoundException("There is no task like this to remove");
        }

        task.Deleted = true;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(int boardId, int taskId, TaskUpdateDto taskDto) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
                    .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot update a task - Board not found.");
        }

        var task = await _context.Tasks
                    .FirstOrDefaultAsync(j => j.Id == taskId && !j.Deleted && j.BoardId == boardId);

        if(task == null) {
            throw new NotFoundException("There is no task like this to update");
        }

        if(taskDto.SectionId != null && taskDto.SectionId != task.SectionId) {
            var newSection = await _context.Sections
                                .FirstOrDefaultAsync(s => s.Id == taskDto.SectionId);

            if( newSection != null && newSection.BoardId == boardId ) {
                task.SectionId = (int) taskDto.SectionId;
            }
            else {
                throw new NotFoundException("Cannot find newly specified section for task.");
            }
        }
        if(taskDto.Name != null) {
            task.Name = taskDto.Name;
        }
        if(taskDto.AssignedTo != null) {
            task.AssignedTo = taskDto.AssignedTo;
        }
        if(taskDto.Position != null) {
            task.Position = (int) taskDto.Position;
        }

        await _context.SaveChangesAsync();
    }
}
