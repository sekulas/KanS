using AutoMapper;
using KanS.Entities;
using KanS.Exceptions;
using KanS.Interfaces;
using KanS.Models;
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

        int nextId = await _context.Boards.CountAsync() + 1;

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
            AssignmentDate = DateTime.UtcNow,
            ParticipatingAccepted = "true"
        };

        await _context.UserBoards.AddAsync(ub);

        await _context.SaveChangesAsync();

        return nextId;
    }

    public async Task<BoardWithSectionsDto> GetBoardById(int boardId) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards.AsNoTracking()
            .Include(ub => ub.Board)
                .ThenInclude(b => b.Sections.Where(s => !s.Deleted))
                    .ThenInclude(s => s.Tasks.Where(t => !t.Deleted))
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "true");

        if(ub == null) {
            throw new NotFoundException("Board not found.");
        }

        var sectionsDto = ub.Board.Sections
            .Select(s => _mapper.Map<SectionDto>(s))
            .ToList();

        foreach(var section in sectionsDto) {
            var targetSection = ub.Board.Sections
                                .FirstOrDefault(s => s.Id == section.Id);

            section.Tasks = targetSection.Tasks
                                .Select(t => _mapper.Map<TaskE, TaskDto>(t))
                                .ToList();
        };

        var boardDto = _mapper.Map<BoardWithSectionsDto>(ub.Board);
        boardDto.Sections = sectionsDto;
        boardDto.Favourite = ub.Favourite;

        return boardDto;
    }

    public async Task UpdateBoard(int boardId, BoardUpdateDto boardDto) {
        var userId = (int) _userContextService.GetUserId;
        var board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == boardId);

        if(board == null) {
            throw new NotFoundException("Board not found.");
        }

        if(boardDto.Name != null) {
            board.Name = boardDto.Name;
        }
        if(boardDto.Description != null) {
            board.Description = boardDto.Description;
        }
        if(boardDto.Favourite != null) {
            var ub = await _context.UserBoards.FirstOrDefaultAsync(b => b.BoardId == boardId && b.UserId == userId);
            if(ub == null) {
                throw new NotFoundException("There is no connection between the user and the board.");
            }
            ub.Favourite = (bool) boardDto.Favourite;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<BoardDto>> GetAllBoardsForUser() {
        var userId = (int) _userContextService.GetUserId;

        var boards = await _context.UserBoards.AsNoTracking()
            .Where(ub => ub.UserId == userId && !ub.Deleted && ub.ParticipatingAccepted == "true")
            .Select(ub => _mapper.Map<Board, BoardDto>(ub.Board))
            .ToListAsync();

        return boards;
    }

    public async Task<List<BoardDto>> GetAllFavouriteBoardsForUser() {
        var userId = (int) _userContextService.GetUserId;

        var boards = await _context.UserBoards.AsNoTracking()
            .Where(ub => ub.UserId == userId && ub.Favourite && !ub.Deleted && ub.ParticipatingAccepted == "true")
            .Select(ub => _mapper.Map<Board, BoardDto>(ub.Board))
            .ToListAsync();

        return boards;
    }

    public async Task<List<BoardDto>> GetAllRequestedParticipationBoardsForUser() {
        var userId = (int) _userContextService.GetUserId;

        var boards = await _context.UserBoards.AsNoTracking()
            .Where(ub => ub.UserId == userId && !ub.Deleted && ub.ParticipatingAccepted == "pending")
            .Select(ub => _mapper.Map<Board, BoardDto>(ub.Board))
            .ToListAsync();

        return boards;
    }

    public async Task RequestForParticipationToBoard(int boardId, UserParticipationRequestDto userDto) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards.AsNoTracking()
            .Include(ub => ub.Board)
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "true");

        if(ub == null) {
            throw new NotFoundException("Board not found.");
        }
        else if(ub.Board.OwnerId != userId) {
            throw new UnauthorizedAccessException("Only the owner of board can request others to participate.");
        }

        var requestedUser = await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == userDto.Email);

        if(requestedUser == null) {
            throw new NotFoundException("There is no user with this email to share with.");
        }

        var checkIfRequestNotSent = await _context.UserBoards
            .FirstOrDefaultAsync(ub => ub.UserId == requestedUser.Id && ub.BoardId == boardId && (ub.ParticipatingAccepted == "true" || ub.ParticipatingAccepted == "pending") );

        if(checkIfRequestNotSent != null) {
            throw new BadRequestException("You have already sent a colaboration request to this user.");
        }

        UserBoard newUb = new UserBoard() {
            UserId = requestedUser.Id,
            BoardId = boardId,
            AssignmentDate = DateTime.UtcNow,
            ParticipatingAccepted = "pending"
        };

        await _context.UserBoards.AddAsync(newUb);

        await _context.SaveChangesAsync();

    }
    public async Task<bool> RespondToParticipationRequest(int boardId, ParticipationRequestResponseDto resDto) {
        var userId = (int) _userContextService.GetUserId;

        var ub = await _context.UserBoards
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && !ub.Deleted && ub.ParticipatingAccepted == "pending");

        if(ub == null) {
            throw new NotFoundException("Request not found.");
        }

        bool res = true;

        if(resDto.ParticipatingAccepted == "true") {
            ub.ParticipatingAccepted = "true";
        }
        else if(resDto.ParticipatingAccepted == "false") {
            ub.ParticipatingAccepted = "false";
            res = false;
        }
        else {
            throw new BadRequestException("Something was wrong with the request response meessage.");
        }

        await _context.SaveChangesAsync();

        return res;
    }

    public async Task RemoveBoard(int boardId) {
        var userId = (int) _userContextService.GetUserId;
        var board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == boardId);

        if(board == null) {
            throw new NotFoundException("Board not found.");
        }

        if(userId == board.OwnerId) {
            var ubList = await _context.UserBoards
                .Where(ub => ub.BoardId == boardId)
                .ToListAsync();

            foreach(var ub in ubList) {
                ub.Deleted = true;
            }
        }
        else {
            var ub = await _context.UserBoards
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BoardId == boardId && ub.ParticipatingAccepted == "true");

            if(ub == null) {
                throw new NotFoundException("There is no connection between the user and the board.");
            }

            ub.ParticipatingAccepted = "false";
        }



        await _context.SaveChangesAsync();
    }
}
