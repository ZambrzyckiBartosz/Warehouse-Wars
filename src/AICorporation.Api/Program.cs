using AICorporation.Api;
using AICorporation.Core.Models;
using Microsoft.EntityFrameworkCore;
using AICorporation.Infrastructure;
using AICorporation.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var game = new Company("firma 1", 5000000m, new List<Building>());
var testWreHouse = new Warehouse(1, 1, 10000,1,10,0);
game.BuyBuilding(testWreHouse, 0);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
builder.Services.AddControllers();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddHostedService<GameEngineService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

