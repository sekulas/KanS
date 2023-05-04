using KanS.Entities;
using KanS.Interfaces;
using KanS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;

[Route("api/board")]
[ApiController]
[Authorize]
public class BoardController : ControllerBase {
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService) {
        _boardService = boardService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateBoard() {

        int boardId = await _boardService.CreateBoard();

        return Created($"/api/board/{boardId}", null);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoardDto>> GetBoardById([FromRoute] int id) {
        var board = await _boardService.GetBoardById(id);

        return Ok(board);
    }
}
