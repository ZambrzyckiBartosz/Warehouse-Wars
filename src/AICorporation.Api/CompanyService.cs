using AICorporation.Core.Models;
using AICorporation.Infrastructure;

namespace AICorporation.Api;
public class CompanyService
{
    private AppDbContext _context;
    public CompanyService(AppDbContext company)
    {
        _context = company;
    }

}