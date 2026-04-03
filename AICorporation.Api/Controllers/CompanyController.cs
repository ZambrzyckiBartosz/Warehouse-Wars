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
    public Company CompanyUpdate([FromBody] BuyWarehouseRequest request)
    {
        var newWarehouse = new Warehouse(2137,request.name,1,50000,10000,31,5);
        _company.BuyBuilding(newWarehouse,42013);

        return _company;
    }
}