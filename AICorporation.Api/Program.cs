using AICorporation.Core.Models;
var builder = WebApplication.CreateBuilder(args);

var game = new Company("firma 1", 5000000m, new List<Building>());
var testWreHouse = new Warehouse(1,"testWreHouse", 1, 10000,1,10,0);
game.BuyBuilding(testWreHouse, 0);

builder.Services.AddSingleton(game);
builder.Services.AddHostedService<GameEngineService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

