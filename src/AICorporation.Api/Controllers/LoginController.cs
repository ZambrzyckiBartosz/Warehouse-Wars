using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = AICorporation.Api.Services.RegisterRequest;

namespace AICorporation.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(Login _company) : ControllerBase
{
    [HttpPost("login")]

    public async Task<ActionResult> LoginUser([FromBody] SendLoginRequest loginRequest)
    {
        await _company.LoginHanlder(loginRequest);
        return Ok();
    }

}