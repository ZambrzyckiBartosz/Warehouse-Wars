using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using AICorporation.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AICorporation.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _company;

    public CompanyController(CompanyService company)
    {
        _company = company;
    }

    [HttpGet]
    public async Task<Company> GetCompanyInfo()
    {
        return await _company.GetCompanyInfo();
    }

     [HttpPost("buy-warehouse")]
    public async Task<ActionResult<Company>> CompanyUpdate([FromBody] BuyWarehouseRequest request )
    {
        var result = await _company.BuyWarehouseHanlder(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterRequest request)
    {
        await _company.RegisterHanlder(request);
        return Ok();
    }

}