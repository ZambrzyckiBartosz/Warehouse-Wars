namespace AICorporation.Core.Models;

public class BuidlingFactory
{
     static public (Warehouse, decimal) BuildNewFactory(BuildingType type, string? name)
     {
          switch (type)
          {
               case BuildingType.smallWarehouse:
                    return (new Warehouse(100, name, 1, 100, 1, 10, 100), 25);
               case BuildingType.mediumWarehouse:
                    return (new Warehouse(50, name, 1, 100, 1, 10, 100),50);
               case BuildingType.largeWarehouse:
                    return (new Warehouse(25, name, 1, 100, 1, 10, 100), 100);
               default:
                    return (new Warehouse(2137, name, 1, 100, 1, 10, 100), 100);
          }
     }
}