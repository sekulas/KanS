using KanS.Entities;
using KanS.Interfaces;
using KanS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<BoardDto>> CreateBoard() {

        var boardId = await _boardService.CreateBoard();

        var board = await _boardService.GetBoardById(boardId);

        return CreatedAtAction(nameof(GetBoardById), new { id = board.Id }, board);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBoard([FromRoute] int id, [FromBody] BoardUpdateDto boardDto) {

        await _boardService.UpdateBoard(id, boardDto);

        return Ok();
    }

    [HttpDelete("{boardId}")]
    public async Task<ActionResult> RemoveBoard([FromRoute] int boardId) {

        await _boardService.RemoveBoard(boardId);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoardDto>> GetBoardById([FromRoute] int id) {
        var board = await _boardService.GetBoardById(id);

        return Ok(board);
    }

    [HttpGet]
    public async Task<ActionResult<List<BoardDto>>> GetAllBoardsForUser() {
        var boards = await _boardService.GetAllBoardsForUser();

        return Ok(boards);
    }

    [HttpGet("favourites")]
    public async Task<ActionResult<List<BoardDto>>> GetAllFavouriteBoardsForUser() {
        var boards = await _boardService.GetAllFavouriteBoardsForUser();

        return Ok(boards);
    }
}
