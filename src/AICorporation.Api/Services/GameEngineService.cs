using AICorporation.Core.Models;
using AICorporation.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
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
                var myCompany = db.Users.FirstOrDefault();
                if (myCompany != null)
                {
                    List<Building> tempBuildings = new List<Building>();
                    foreach (var building in myCompany.inventory)
                    {
                        for (int i = 0; i < building.quantity; i++)
                        {
                            tempBuildings.Add(new Warehouse());
                        }
                    }
                    var tempCompany = new Company(myCompany.CompanyName,myCompany.ComapnyBalance);
                    tempCompany.ReceiveIncome(tempCompany.TotalIncome());
                    myCompany.ComapnyBalance = tempCompany.CompanyBalance;
                    Console.WriteLine($"{myCompany.CompanyName} {myCompany.ComapnyBalance}");
                    db.SaveChanges();
                }
            }
            await Task.Delay(1000, stoppingToken);

        }

    }
}