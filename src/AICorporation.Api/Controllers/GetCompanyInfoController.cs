using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using AICorporation.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AICorporation.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class GetCompanyInfoController(GetCompanyInfo _company) : ControllerBase
{
    [HttpGet]
    public async Task<Company> GetCompanyInfo()
    {
        return await _company.GetCompanyInfoAsync();
    }

}