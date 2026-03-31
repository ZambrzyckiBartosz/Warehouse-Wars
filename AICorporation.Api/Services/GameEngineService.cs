
public class GameEngineService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("working...");
            await Task.Delay(1000, stoppingToken);
        }

    }
}