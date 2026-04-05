using System.Security.Claims;
using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using AICorporation.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AICorporation.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class BuyWarehouseController(BuyWarehouse _company) : ControllerBase
{
    [Authorize]
    [HttpPost("buy-warehouse")]
    public async Task<ActionResult<Company>> CompanyUpdate([FromBody] BuyWarehouseRequest request )
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userID == null) return BadRequest();
        var result = await _company.BuyWarehouseAsync(request, int.Parse(userID));
        return Ok(result);
    }

}