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
}