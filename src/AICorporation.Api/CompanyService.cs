using AICorporation.Core.Models;
using AICorporation.Infrastructure;
public class CompanyService
{
    private AppDbContext _context;
    public CompanyService(AppDbContext company)
    {
        _context = company;
    }

    public bool ProcessWarehousePurchase(BuyWarehouseRequest request)
    {
        return true;
    }
}