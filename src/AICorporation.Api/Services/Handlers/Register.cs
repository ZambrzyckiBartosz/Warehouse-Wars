using AICorporation.Core.Models;
using AICorporation.Infrastructure;

namespace AICorporation.Api.Services;

public class Register(AppDbContext _context)
{
    public async Task RegisterHanlder(RegisterRequest request)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _context.Users.Add(new User{username = request.Username, password = hashPassword,
            CompanyName = request.CompanyName, ComapnyBalance = 5000000, inventory = new List<Inventory>()
        });
        await _context.SaveChangesAsync();
    }

}