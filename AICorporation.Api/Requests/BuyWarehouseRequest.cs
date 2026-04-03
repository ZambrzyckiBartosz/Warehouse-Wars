using System.ComponentModel.DataAnnotations;

public class BuyWarehouseRequest
{
    [Required]
    [StringLength(50,MinimumLength = 3)]
    public string? Name { get; set; }

}