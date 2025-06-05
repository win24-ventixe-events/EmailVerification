using System.ComponentModel.DataAnnotations;

namespace VentixeEmail.Models;

public class VerificationCodeRequest
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Code { get; set; } = null!;
    
    public TimeSpan ValidFor { get; set; }
}