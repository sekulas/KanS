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
    public async Task<ActionResult<BoardDto>> CreateBoard() {

        var boardId = await _boardService.CreateBoard();

        var board = await _boardService.GetBoardById(boardId);

        return CreatedAtAction(nameof(CreateBoard), new { id = board.Id }, board);
    }

    [HttpPut("{sectionId}")]
    public async Task<ActionResult> UpdateBoard([FromRoute] int sectionId, [FromBody] BoardUpdateDto boardDto) {

        await _boardService.UpdateBoard(sectionId, boardDto);

        return Ok();
    }

    [HttpDelete("{boardId}")]
    public async Task<ActionResult> RemoveBoard([FromRoute] int boardId) {

        await _boardService.RemoveBoard(boardId);

        return NoContent();
    }

    [HttpGet("{boardId}")]
    public async Task<ActionResult<BoardWithSectionsDto>> GetBoardById([FromRoute] int boardId) {
        var board = await _boardService.GetBoardById(boardId);

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

    [HttpPost("{boardId}/request")]
    public async Task<ActionResult> RequestForParticipationToBoard([FromRoute] int boardId, [FromBody] UserParticipationRequestDto userDto) {

        await _boardService.RequestForParticipationToBoard(boardId, userDto);

        return Ok();
    }

    [HttpPut("{boardId}/request")]
    public async Task<ActionResult> RespondToParticipationRequest([FromRoute] int boardId, [FromBody] ParticipationRequestResponseDto resDto) {

        bool isAccessGranted = await _boardService.RespondToParticipationRequest(boardId, resDto);

        if(isAccessGranted) {
            return Ok();
        }

        return NoContent();
    }

    [HttpGet("request")]
    public async Task<ActionResult<List<BoardDto>>> GetAllRequestedParticipationBoardsForUser() {
        var boards = await _boardService.GetAllRequestedParticipationBoardsForUser();

        return Ok(boards);
    }
}
