using KanS.Interfaces;
using KanS.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;

[Route("api/board")]
[ApiController]
public class BoardController : ControllerBase {
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService) {
        _boardService = boardService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateBoard() {

        await _boardService.CreateBoard();

        return Ok();

    }
}
