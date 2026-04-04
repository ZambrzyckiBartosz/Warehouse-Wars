using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AICorporation.Infrastructure;
using System.Text;
using AICorporation.Api.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace AICorporation.Api.Services;
public class AuthService
{
    private AppDbContext _context;
    private IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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
        var keyByteArray = Encoding.UTF8.GetBytes(_configuration["JwtKey"] ?? "elo320potrzeba32znakowtodolozejeszczepareboczemuniexd67");
        var key = new SymmetricSecurityKey(keyByteArray);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token =  tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}