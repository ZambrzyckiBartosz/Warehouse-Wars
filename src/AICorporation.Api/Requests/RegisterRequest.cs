using System.ComponentModel.DataAnnotations;
namespace AICorporation.Api.Services;
public class RegisterRequest
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? CompanyName { get; set; }
}