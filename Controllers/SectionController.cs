using KanS.Entities;
using KanS.Interfaces;
using KanS.Models;
using KanS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;

[Route("api/board/{boardId}/section")]
[ApiController]
[Authorize]
public class SectionController : ControllerBase {
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService) {
        _sectionService = sectionService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Section>> CreateSection([FromRoute] int boardId) {

        var sectionId = await _sectionService.CreateSection(boardId);

        var section = await _sectionService.GetSectionById(sectionId);

        return Ok(section);
    }

    [HttpPut("{sectionId}")]
    public async Task<ActionResult> UpdateSection([FromRoute] int sectionId, [FromBody] SectionUpdateDto sectionDto) {

        await _sectionService.UpdateSection(sectionId, sectionDto);

        return Ok();
    }

    [HttpDelete("{sectionId}")]
    public async Task<ActionResult> RemoveSection([FromRoute] int boardId, [FromRoute] int sectionId) {

        await _sectionService.RemoveSection(sectionId, boardId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<SectionDto>> GetSectionById(int sectionId) {

        var sectionDto = await _sectionService.GetSectionById(sectionId);

        return Ok(sectionDto);
    }

}
