using AutoMapper;
using KanS.Entities;
using KanS.Exceptions;
using KanS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanS.Services;

public class SectionService : ISectionService {
    private readonly KansDbContext _context;
    private readonly IUserContextService _userContextService;

    public SectionService(KansDbContext context, IUserContextService userContextService) {
        _context = context;
        _userContextService = userContextService;
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

    public async Task<Section> GetSectionById(int sectionId) {
        var section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == sectionId);

        if(section == null) {
            throw new NotFoundException("Section not found.");
        }

        return section;
    }
}
