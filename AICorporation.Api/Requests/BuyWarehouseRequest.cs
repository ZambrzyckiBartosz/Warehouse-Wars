using System.ComponentModel.DataAnnotations;
using AICorporation.Core.Models;
public class BuyWarehouseRequest
{
    [Required]
    [StringLength(50,MinimumLength = 3)]
    public string? Name { get; set; }
    public BuildingType ? Type { get; set; }

}