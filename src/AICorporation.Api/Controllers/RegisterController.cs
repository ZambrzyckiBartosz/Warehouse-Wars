using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = AICorporation.Api.Services.RegisterRequest;

namespace AICorporation.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController(Register _company) : ControllerBase
{
    [HttpPost]

    public async Task<ActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
        await _company.RegisterHanlder(registerRequest);
        return Ok();
    }

}