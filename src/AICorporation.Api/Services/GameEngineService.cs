using AICorporation.Core.Models;
using AICorporation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AICorporation.Api.Services;
public class GameEngineService : BackgroundService
{
    public IServiceScopeFactory _scopeFactory { get; }

    public GameEngineService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var myCompany = db?.Users.Include(u => u.inventory).OrderBy(u=> u.id).FirstOrDefault();
                if (myCompany != null)
                {
                    List<Building> tempBuildings = new List<Building>();
                    if (myCompany.inventory != null)
                        foreach (var building in myCompany.inventory)
                        {
                            tempBuildings.Add(BuidlingFactory.BuildNewFactory((BuildingType)building.type).Item1);
                        }

                    if (myCompany.CompanyName != null)
                    {
                        var tempCompany = new Company(myCompany.CompanyName,myCompany.ComapnyBalance, tempBuildings);
                        tempCompany.ReceiveIncome(tempCompany.TotalIncome());
                        if (myCompany != null)
                        {
                            myCompany.ComapnyBalance = tempCompany.CompanyBalance;
                            Console.WriteLine($"{myCompany.CompanyName} {myCompany.ComapnyBalance}");
                        }
                    }

                    db?.SaveChanges();
                }
            }
            await Task.Delay(1000, stoppingToken);

        }

    }
}