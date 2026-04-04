using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using AICorporation.Core.Models;
using AICorporation.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace AICorporation.Api;
public class CompanyService
{
    private AppDbContext _context;
    public CompanyService(AppDbContext company)
    {
        _context = company;
    }

    public async Task<Company> GetCompanyInfo()
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

    public async Task<Company> BuyWarehouseHanlder(BuyWarehouseRequest request)
    {
        var myCompany = await _context.Users.Include(u => u.inventory).OrderBy(u => u.id).FirstOrDefaultAsync();
        if (myCompany == null) return new Company("straszna chujnia" ?? "jakis noname", 420, new List<Building>());
        if (!(request.Type.HasValue && request.Type.Value > 0))
        {
            Console.WriteLine("request error");
            return new Company("request problem?", 420, new List<Building>());
        }
        var buildFactory = BuidlingFactory.BuildNewFactory(request.Type.Value);
        var tempBuildings = new List<Building>();

        if (myCompany.inventory == null)
        {
            myCompany.inventory = new List<Inventory>();
        }

        foreach (var building in myCompany.inventory)
        {
            tempBuildings.Add(BuidlingFactory.BuildNewFactory((BuildingType)building.type).Item1);
        }
        if (myCompany.ComapnyBalance < buildFactory.Item2) return new Company(myCompany.CompanyName ?? "jakis noname", myCompany.ComapnyBalance,tempBuildings);
        tempBuildings.Add(BuidlingFactory.BuildNewFactory((BuildingType)request.Type).Item1);
        myCompany.ComapnyBalance -= buildFactory.Item2;
        myCompany.inventory.Add(new Inventory{userid = myCompany.id, type = (int)request.Type.Value, level = 1});
        await _context.SaveChangesAsync();
        return new Company(myCompany.CompanyName ?? "jakis noname", myCompany.ComapnyBalance, tempBuildings);
    }

    public async Task RegisterHanlder(RegisterRequest request)
    {
        _context.Users.Add(new User{username = request.Username, password = request.Password,
            CompanyName = request.CompanyName, ComapnyBalance = 5000000, inventory = new List<Inventory>()
    });
        await _context.SaveChangesAsync();
    }
}