

public class Warehouse : Building
{
       public Warehouse(int id, string? name, int level,int maxCapacity,int currentStock, decimal revenuePerItem, decimal baseMaintanceCost)
       {
              ID = id;
              Name = name;
              Level = level;
              MaxCapacity = maxCapacity;
              CurrentStock = currentStock;
              RevenuePerItem = revenuePerItem;
              BaseMaintanceCost = baseMaintanceCost;
       }

       //For Database connection
       private Warehouse()
       {

       }
       public int MaxCapacity { get; private set; }
       public int CurrentStock { get; private set; }
       public decimal RevenuePerItem { get; private set; }
       public decimal BaseMaintanceCost { get; private set; }

       public override decimal CalculateRevenue()
       {
              return CurrentStock * RevenuePerItem;
       }

       public override decimal CalculateIncome()
       {
              return (CurrentStock * RevenuePerItem) -  BaseMaintanceCost;
       }

       public override decimal CalculateArea()
       {
              return MaxCapacity * Level;
       }
}