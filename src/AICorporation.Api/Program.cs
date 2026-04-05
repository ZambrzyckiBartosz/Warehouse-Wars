using Microsoft.AspNetCore.Authentication.JwtBearer;
using AICorporation.Core.Models;
using Microsoft.EntityFrameworkCore;
using AICorporation.Infrastructure;
using AICorporation.Api.Services;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string? jwtKey = builder.Configuration["JwtKey"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentException("JWTKey must be provided");
}
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
    };
});
var game = new Company("firma 1", 5000000m, new List<Building>());
var testWreHouse = new Warehouse(1, 1, 10000,1,10,0);
game.BuyBuilding(testWreHouse, 0);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
builder.Services.AddControllers();
builder.Services.AddScoped<BuyWarehouse>();
builder.Services.AddScoped<GetCompanyInfo>();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<Register>();
builder.Services.AddHostedService<GameEngineService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

