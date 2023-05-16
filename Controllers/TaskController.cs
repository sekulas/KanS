using KanS.Interfaces;
using KanS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;


[Route("api/board/{boardId}/section/{sectionId}/task")]
[ApiController]
[Authorize]
public class TaskController : ControllerBase {
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService) {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask([FromRoute] int boardId, [FromRoute] int sectionId) {

        var taskId = await _taskService.CreateTask(boardId, sectionId);

        var taskDto = await _taskService.GetTaskById(boardId, taskId);

        return CreatedAtAction(nameof(CreateTask), new { id = taskId }, taskDto);
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult<TaskDto>> GetTaskById([FromRoute] int boardId, [FromRoute] int taskId) {

        var taskDto = await _taskService.GetTaskById(boardId, taskId);

        return Ok(taskDto);
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> RemoveTask([FromRoute] int boardId, [FromRoute] int taskId) {

        await _taskService.RemoveTask(boardId, taskId);

        return NoContent();
    }

    [HttpPut("{taskId}")]
    public async Task<ActionResult> UpdateTask([FromRoute] int boardId, [FromRoute] int taskId, TaskUpdateDto taskDto) {

        await _taskService.UpdateTask(boardId, taskId, taskDto);

        return Ok();
    } 
}
