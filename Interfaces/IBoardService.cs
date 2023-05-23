using KanS.Entities;
using KanS.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Interfaces;

public interface IBoardService {
    Task<int> CreateBoard();
    Task<BoardWithSectionsDto> GetBoardById(int boardId);
    Task<List<BoardDto>> GetAllBoardsForUser();
    Task<List<BoardDto>> GetAllFavouriteBoardsForUser();
    Task<List<BoardDto>> GetAllRequestedParticipationBoardsForUser();
    Task RequestForParticipationToBoard(int boardId, UserParticipationRequestDto userDto);
    Task<bool> RespondToParticipationRequest(int boardId, ParticipationRequestResponseDto resDto);
    Task UpdateBoard(int boardId, BoardUpdateDto boardDto);
    Task RemoveBoard(int boardId);
}
