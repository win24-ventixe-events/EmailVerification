using System.ComponentModel.DataAnnotations;

namespace VentixeEmail.Models;

public class VerifyCode
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
}