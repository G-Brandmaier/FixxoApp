using System.ComponentModel.DataAnnotations;

namespace WebAppFrontend.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "An email is required")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A password is required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
