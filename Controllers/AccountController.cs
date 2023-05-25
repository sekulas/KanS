using AutoMapper;
using KanS.Models;
using KanS.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase {
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper) {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDto dto) {

        await _accountService.RegisterUser(dto);

        UserLoginDto loginDto = _mapper.Map<UserLoginDto>(dto);

        string writtenToken = await _accountService.GenerateJwt(loginDto);

        return Ok(new { token = writtenToken });
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto) {

        string writtenToken = await _accountService.GenerateJwt(dto);

        return Ok(new { token = writtenToken });
    }
}