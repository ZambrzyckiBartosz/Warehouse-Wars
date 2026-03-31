namespace AICorporation.Core.Models;
public class Company
{
    public string CompanyName { get; init; }
    public decimal CompanyBalance { get; private set; }
    public List<Building> Buildings { get; private set; }

    public Company(string companyName, decimal companyBalance, List<Building> buildings)
    {
        CompanyName = companyName;
        CompanyBalance = companyBalance;
        Buildings = new List<Building>(buildings);
    }

    public bool BuyBuilding(Building building, decimal cost)
    {
        if (cost > CompanyBalance) return false;
        Buildings.Add(building);
        CompanyBalance -= cost;
        return true;
    }

    public void ReceiveIncome(decimal income)
    {
        CompanyBalance += income;
    }

    public decimal TotalIncome()
    {
        return Buildings.Sum(b => b.CalculateIncome());
    }
}