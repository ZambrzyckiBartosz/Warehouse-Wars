using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using AICorporation.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AICorporation.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class BuyWarehouseController(BuyWarehouse _company) : ControllerBase
{

    [HttpPost("buy-warehouse")]
    public async Task<ActionResult<Company>> CompanyUpdate([FromBody] BuyWarehouseRequest request )
    {
        var result = await _company.BuyWarehouseAsync(request);
        return Ok(result);
    }

}