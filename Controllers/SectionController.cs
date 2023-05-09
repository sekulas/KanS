using KanS.Entities;
using KanS.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult<Section>> GetSectionById(int sectionId) {

        var section = await _sectionService.GetSectionById(sectionId);

        return Ok(section);
    }

}
