using AICorporation.Core.Models;
using AICorporation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AICorporation.Api.Services;

public class GetCompanyInfo(AppDbContext _context)
{
    public async Task<Company> GetCompanyInfoAsync()
    {
        var myCompany = await _context.Users.Include(u => u.inventory).OrderBy(u => u.id).FirstOrDefaultAsync();
        if (myCompany == null) return new Company("chujnia",2137,new List<Building>());
        var tempBuildings = new List<Building>();

        if (myCompany.inventory == null) myCompany.inventory = new List<Inventory>();
        foreach (var building in myCompany.inventory)
        {
            tempBuildings.Add(BuidlingFactory.BuildNewFactory((BuildingType)building.type).Item1);
        }
        return new Company(myCompany.CompanyName ?? "jakis noname", myCompany.ComapnyBalance, tempBuildings);
    }
}