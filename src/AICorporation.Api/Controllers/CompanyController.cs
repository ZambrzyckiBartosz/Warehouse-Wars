using AICorporation.Api.Requests;
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
    public Company GetCompanyInfo()
    {
        return _company.GetCompanyInfo();
    }

     [HttpPost("buy-warehouse")]
    public ActionResult<Company> CompanyUpdate([FromBody] BuyWarehouseRequest request )
    {
        var result = _company.BuyWarehouseHanlder(request);
        return Ok(result);
    }

}