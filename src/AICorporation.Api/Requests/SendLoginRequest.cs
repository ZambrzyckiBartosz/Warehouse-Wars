using System.ComponentModel.DataAnnotations;

namespace AICorporation.Api.Requests;

public class SendLoginRequest
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Password { get; set; }
}