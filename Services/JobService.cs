using AutoMapper;
using KanS.Entities;
using KanS.Exceptions;
using KanS.Interfaces;
using KanS.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace KanS.Services;

public class JobService : IJobService {
    private readonly KansDbContext _context;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public JobService(KansDbContext context, IUserContextService userContextService, IMapper mapper) {
        _context = context;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<int> CreateJob(int boardId, int sectionId) {
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

        int position = section.Tasks.Count() + 1;

        int nextId = await _context.Jobs.CountAsync() + 1;

        Job job = new Job() {
            Id = nextId,
            BoardId = boardId,
            SectionId = sectionId,
            Position = position,
        };

        await _context.Jobs.AddAsync(job);

        await _context.SaveChangesAsync();

        return nextId;
    }

    public async Task<JobDto> GetJobById(int boardId, int jobId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot get a task - Board not found.");
        }

        var job = await _context.Jobs
            .FirstOrDefaultAsync(j => j.Id == jobId && !j.Deleted && j.BoardId == boardId);

        if(job == null) {
            throw new NotFoundException("Task not found.");
        }

        var jobDto = _mapper.Map<JobDto>(job);

        return jobDto;
    }

    public async Task RemoveJob(int boardId, int jobId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
                    .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);
        
        if(ub == null) {
            throw new NotFoundException("Cannot remove a task - Board not found.");
        }

        var job = await _context.Jobs
            .FirstOrDefaultAsync(j => j.Id == jobId && !j.Deleted && j.BoardId == boardId);

        if( job == null) {
            throw new NotFoundException("There is no task like this to remove");
        }

        job.Deleted = true;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateJob(int boardId, int jobId, JobUpdateDto jobDto) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
                    .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot update a task - Board not found.");
        }

        var job = await _context.Jobs
                    .FirstOrDefaultAsync(j => j.Id == jobId && !j.Deleted && j.BoardId == boardId);

        if(job == null) {
            throw new NotFoundException("There is no task like this to update");
        }

        if(jobDto.SectionId != null) {
            var newSection = await _context.Sections
                                .FirstOrDefaultAsync(s => s.Id == jobDto.SectionId);

            if( newSection != null && newSection.BoardId == boardId ) {
                job.SectionId = (int) jobDto.SectionId;
            }
            else {
                throw new NotFoundException("Cannot find newly specified section for task.");
            }
        }
        if(jobDto.Name != null) {
            job.Name = jobDto.Name;
        }
        if(jobDto.AssignedTo != null) {
            job.AssignedTo = jobDto.AssignedTo;
        }
        if(jobDto.Position != null) {
            job.Position = (int) jobDto.Position;
        }

        await _context.SaveChangesAsync();
    }
}
