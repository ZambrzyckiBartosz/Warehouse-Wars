using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = AICorporation.Api.Services.RegisterRequest;

namespace AICorporation.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _company;

    private AuthController(AuthService company)
    {
        _company = company;
    }
    [HttpPost("register")]

    public async Task<ActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
        await _company.RegisterHanlder(registerRequest);
        return Ok();
    }

    [HttpPost("login")]

    public async Task<ActionResult> LoginUser([FromBody] SendLoginRequest loginRequest)
    {
        await _company.LoginHanlder(loginRequest);
        return Ok();
    }

}