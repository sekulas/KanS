using AutoMapper;
using KanS.Entities;
using KanS.Exceptions;
using KanS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanS.Services;

public class BoardService : IBoardService {
    private readonly KansDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;

    public BoardService(KansDbContext context, IMapper mapper, IUserContextService userContextService) {
        _context = context;
        _mapper = mapper;
        _userContextService = userContextService;
    }
    public async Task<int> CreateBoard() {

        var userId = (int) _userContextService.GetUserId;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        int nextId = 1;
        bool anyBoardsExist = await _context.Boards.AnyAsync();

        if(anyBoardsExist) {
            int maxId = await _context.Boards.MaxAsync(u => u.Id);
            nextId = maxId + 1;
        }

        Board board = new Board() {
            Id = nextId,
            Name = $"New Board #{user.BoardsCreated + 1}",
            OwnerId = userId,
        };

        user.BoardsCreated++;

        await _context.Boards.AddAsync(board);

        UserBoard ub = new UserBoard() {
            UserId = userId,
            BoardId = nextId,
            AssignmentDate = DateTime.UtcNow
        };

        await _context.UserBoards.AddAsync(ub);

        await _context.SaveChangesAsync();

        return nextId;
    }

    public async Task<BoardDto> GetBoardById(int boardId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .Include(ub => ub.Board)
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted);

        if(ub == null) {
            throw new NotFoundException("There is no connection between the user and the board.");
        }

        var boardDto = _mapper.Map<BoardDto>(ub.Board);

        return boardDto;
    }

    public async Task UpdateBoard(int boardId, BoardUpdateDto boardDto) {

        var board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == boardId);

        if(board == null) {
            throw new NotFoundException("Board not found");
        }

        if(boardDto.Name != null) {
            board.Name = boardDto.Name == "" ? "Untitled" : boardDto.Name;
        }
        if(boardDto.Description != null) {
            board.Description = boardDto.Description;
        }
        if(boardDto.Favourite != null) {
            board.Favourite = (bool) boardDto.Favourite;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<BoardDto>> GetAllBoardsForUser() {
        var userId = (int) _userContextService.GetUserId;

        var boards = await _context.UserBoards
            .Where(ub => ub.UserId == userId && !ub.Deleted)
            .Select(ub => _mapper.Map<Board,BoardDto>(ub.Board))
            .ToListAsync();

        return boards;
    }

    public async Task<List<BoardDto>> GetAllFavouriteBoardsForUser() {
        var userId = (int) _userContextService.GetUserId;

        var boards = await _context.UserBoards
            .Where(ub => ub.UserId == userId && ub.Board.Favourite && !ub.Deleted)
            .Select(ub => _mapper.Map<Board, BoardDto>(ub.Board))
            .ToListAsync();

        return boards;
    }

    public async Task RemoveBoard(int boardId) {
        var userId = (int) _userContextService.GetUserId;
        var board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == boardId);

        if(board == null) {
            throw new NotFoundException("Board not found.");
        }

        if (userId == board.OwnerId) {
            var ubList = await _context.UserBoards
                .Where(ub => ub.BoardId == boardId)
                .ToListAsync();

            foreach(var ub in ubList) {
                ub.Deleted = true;
            }
        }
        else {
            var ub = await _context.UserBoards
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId);

            if(ub == null) {
                throw new NotFoundException("There is no connection between the user and the board.");
            }

            ub.Deleted = true;
        }



        await _context.SaveChangesAsync();
    }
}
