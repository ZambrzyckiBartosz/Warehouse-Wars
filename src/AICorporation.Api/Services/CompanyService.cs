using System.IdentityModel.Tokens.Jwt;
using AICorporation.Api.Requests;
using AICorporation.Api.Services;
using AICorporation.Core.Models;
using AICorporation.Infrastructure;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AICorporation.Api.Services;
public class CompanyService
{
    private AppDbContext _context;
    private IConfiguration _configuration;
    public CompanyService(AppDbContext company, IConfiguration configuration)
    {
        _context = company;
        _configuration = configuration;
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
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _context.Users.Add(new User{username = request.Username, password = hashPassword,
            CompanyName = request.CompanyName, ComapnyBalance = 5000000, inventory = new List<Inventory>()
    });
        await _context.SaveChangesAsync();
    }

    public async Task<string> LoginHanlder(SendLoginRequest request)
    {
        var myUser = await _context.Users.FirstOrDefaultAsync(u => u.username == request.Name);
        if (myUser == null)
        {
            throw new Exception("user not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, myUser.password))
        {
            throw new Exception("password does not match");
        }

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, myUser.id.ToString()) };
        var keyByteArray = Encoding.ASCII.GetBytes(_configuration["JwtKey"] ?? "elo");
        var key = new SymmetricSecurityKey(keyByteArray);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token =  tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}