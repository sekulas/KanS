using KanS.Interfaces;
using KanS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;


[Route("api/board/{boardId}/section/{sectionId}/task")]
[ApiController]
[Authorize]
public class JobController : ControllerBase {
    private readonly IJobService _jobService;

    public JobController(IJobService jobService) {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<ActionResult<JobDto>> CreateJob([FromRoute] int boardId, [FromRoute] int sectionId) {

        var jobId = await _jobService.CreateJob(boardId, sectionId);

        var jobDto = await _jobService.GetJobById(boardId, jobId);

        return CreatedAtAction(nameof(CreateJob), new { id = jobDto.Id }, jobDto);
    }

    [HttpGet("{jobId}")]
    public async Task<ActionResult<JobDto>> GetJobById([FromRoute] int boardId, [FromRoute] int jobId) {

        var jobDto = await _jobService.GetJobById(boardId, jobId);

        return Ok(jobDto);
    }

    [HttpDelete("{jobId}")]
    public async Task<ActionResult> RemoveSection([FromRoute] int boardId, [FromRoute] int jobId) {

        await _jobService.RemoveJob(boardId, jobId);

        return NoContent();
    }
}
