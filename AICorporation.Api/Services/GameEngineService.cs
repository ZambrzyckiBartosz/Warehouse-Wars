using AICorporation.Core.Models;
public class GameEngineService : BackgroundService
{
    private readonly Company _company;

    public GameEngineService(Company company)
    {
        _company = company;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"Current balance: {_company.CompanyBalance}");
            await Task.Delay(1000, stoppingToken);
            _company.ReceiveIncome(_company.TotalIncome());

        }

    }
}