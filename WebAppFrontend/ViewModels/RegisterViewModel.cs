using System.ComponentModel.DataAnnotations;

namespace WebAppFrontend.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "A first name is required")]
    [Display(Name = "First Name")]
    [MinLength(2)]
    [RegularExpression(@"^[a-öA-Ö]+(?:['´-][a-öA-Ö]+)*$", ErrorMessage = "Not a valid first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "A last name is required")]
    [Display(Name = "Last Name")]
    [MinLength(2)]
    [RegularExpression(@"^[a-öA-Ö]+(?:['´-][a-öA-Ö]+)*$", ErrorMessage = "Not a valid last name")]
    public string LastName { get; set;} = null!;

    [Required(ErrorMessage = "An email is required")]
    [Display(Name = "Email")]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Not a valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A password is required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Not a valid password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Confirmation of password is required")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; } = null!;

    public string? RoleName { get; set; } = "product manager";

}
