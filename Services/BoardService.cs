using KanS.Entities;
using KanS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace KanS.Services;

public class BoardService : IBoardService {
    private readonly KansDbContext _context;
    private readonly IUserContextService _userContextService;

    public BoardService(KansDbContext context, IUserContextService userContextService) {
        _context = context;
        _userContextService = userContextService;
    }
    public async Task<int> CreateBoard() {

        var userId = (int) _userContextService.GetUserId;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        int nextId = 1;
        int maxId = await _context.Boards.MaxAsync(u => u.Id);
        if(maxId != null) {
            nextId = maxId + 1;
        }

        Board board = new Board() {
            Id = nextId,
            OwnerId = userId,
        };

        await _context.Boards.AddAsync(board);

        int nextPosition = 1;
        int maxPosition = await _context.UserBoards
            .Where(ub => ub.UserId == userId)
            .MaxAsync(ub => ub.Position);
        if(maxPosition != null) {
            nextPosition = maxPosition + 1;
        }

        UserBoard ub = new UserBoard() {
            UserId = userId,
            BoardId = nextId,
            Position = nextPosition,
        };

        await _context.UserBoards.AddAsync(ub);

        await _context.SaveChangesAsync();

        return nextId;
    }
}
