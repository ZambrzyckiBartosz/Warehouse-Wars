
namespace AICorporation.Core.Models;

public class User
{
    public int id { get; set; }
    public string? username { get; set; }
    public string? password { get; set; }

    public string? CompanyName { get; set; }

    public decimal ComapnyBalance { get; set; }
    public List<Inventory>? inventory { get; set; }
}