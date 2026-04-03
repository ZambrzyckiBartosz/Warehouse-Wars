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
        if (request.Name != null && request.Type != null)
        {
            var resultFactory = BuidlingFactory.BuildNewFactory(request.Type.Value,  request.Name);
            _company.BuyBuilding(resultFactory.Item1, resultFactory.Item2);
        }

        return Ok(_company);
    }
}