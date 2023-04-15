using KanS.Models;
using KanS.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase {
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService) {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDto dto) {
        await _accountService.RegisterUser(dto);
        return Ok();
    }
}