using Microsoft.EntityFrameworkCore;
using AICorporation.Core.Models;
namespace AICorporation.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :  base(options) {}
    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Users_Inventory { get; set; }
}