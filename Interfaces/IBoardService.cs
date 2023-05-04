using KanS.Entities;
using KanS.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Interfaces;

public interface IBoardService {
    Task<int> CreateBoard();
    Task<BoardDto> GetBoardById(int id);
    Task<List<BoardDto>> GetAllBoardsForUser();
}
