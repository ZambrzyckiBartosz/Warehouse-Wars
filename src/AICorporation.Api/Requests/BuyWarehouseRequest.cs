using System.ComponentModel.DataAnnotations;
using AICorporation.Core.Models;

namespace AICorporation.Api.Requests;
public class BuyWarehouseRequest
{
    [Required]
    [StringLength(50,MinimumLength = 3)]
    public string? Name { get; set; }
    [Required]
    public BuildingType ? Type { get; set; }

}