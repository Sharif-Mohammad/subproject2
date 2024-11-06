using System.ComponentModel.DataAnnotations;

namespace Auth.Models;

public class RegisterModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}