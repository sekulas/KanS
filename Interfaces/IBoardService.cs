using KanS.Entities;
using KanS.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Interfaces;

public interface IBoardService {
    Task<int> CreateBoard();
    Task<BoardDto> GetBoardById(int boardId);
    Task<List<BoardDto>> GetAllBoardsForUser();
    Task<List<BoardDto>> GetAllFavouriteBoardsForUser();
    Task UpdateBoard(int boardId, BoardUpdateDto boardDto);
    Task RemoveBoard(int boardId);
}
