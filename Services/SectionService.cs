using AutoMapper;
using KanS.Entities;
using KanS.Exceptions;
using KanS.Interfaces;
using KanS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanS.Services;

public class SectionService : ISectionService {
    private readonly KansDbContext _context;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public SectionService(KansDbContext context, IUserContextService userContextService, IMapper mapper) {
        _context = context;
        _userContextService = userContextService;
        _mapper = mapper;
    }
    public async Task<int> CreateSection(int boardId) {
        var board = await _context.Boards
            .Include(b => b.Sections)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        if(board == null) {
            throw new NotFoundException("Cannot add section to non-existing board.");
        }

        int nextId = board.Sections.Count + 1;

        Section section = new Section() {
            BoardId = boardId,
            Name = $"New Section #{nextId}"
        };

        board.Sections.Add(section);

        await _context.SaveChangesAsync();

        return nextId;
    }

    public async Task<SectionDto> GetSectionById(int boardId, int sectionId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot get a section - Board not found.");
        }
        var section = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == sectionId && !s.Deleted);

        if(section == null) {
            throw new NotFoundException("Section not found.");
        }

        var sectionDto = _mapper.Map<SectionDto>(section);

        return sectionDto;
    }

    public async Task RemoveSection(int boardId, int sectionId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .Include(ub => ub.Board)
                .ThenInclude(b => b.Sections)
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot remove a section - Board not found.");
        }

        var section = ub.Board.Sections
            .FirstOrDefault(s => s.Id == sectionId && !s.Deleted);

        if(section == null) {
            throw new NotFoundException("There is no section like this to remove");
        }

        section.Deleted = true;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateSection([FromRoute] int sectionId, [FromBody] SectionUpdateDto sectionDto) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == sectionDto.BoardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("Cannot update a section - Board not found.");
        }

        var section = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == sectionId && !s.Deleted);

        if(section == null) {
            throw new NotFoundException("Section to update not found.");
        }

        if(sectionDto.Name != null) {
            section.Name = sectionDto.Name == "" ? "Untitled" : sectionDto.Name;
        }

        await _context.SaveChangesAsync();
    }
}
