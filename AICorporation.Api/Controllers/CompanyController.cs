using AICorporation.Core.Models;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly Company _company;

    public CompanyController(Company company)
    {
        _company = company;
    }

    [HttpGet]
    public Company GetCompanyInfo()
    {
        return _company;
    }

    [HttpPost("buy-warehouse")]
    public ActionResult<Company> CompanyUpdate([FromBody] BuyWarehouseRequest request)
    {
        var newWarehouse = new Warehouse(2137,request.name,1,50000,10000,31,5);
        var newWarehouseCost = 4213;

        if(!_company.BuyBuilding(newWarehouse,newWarehouseCost)) return BadRequest("U are poor guy");

        return Ok(_company);
    }
}