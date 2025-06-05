using System.ComponentModel.DataAnnotations;

namespace VentixeEmail.Models;

public class CodeRequest
{
    [Required]
    public string Email { get; set; } = null!;
}