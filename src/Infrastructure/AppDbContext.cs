using Microsoft.EntityFrameworkCore;

namespace AICorporation.Infrastructure;
public class User
{
    public int id { get; set; }
    public string? username { get; set; }
    public string? password { get; set; }

    public string? CompanyName { get; set; }

    public decimal ComapnyBalance { get; set; }
    public List<Inventory>? inventory { get; set; }
}

public class Inventory
{
    public int id { get; set; }
    public int userid { get; set; }

    public int type { get; set; }
    public int level { get; set; }

}
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :  base(options) {}
    public DbSet<User> Users { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Inventory> Users_Inventory { get; set; }
}