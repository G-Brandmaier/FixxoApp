using System.ComponentModel.DataAnnotations;

namespace WebAppFrontend.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "A name is required")]
    [MinLength(2)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "An email is required")]
    [MinLength(2)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Not a valid email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "An message is required")]
    [MinLength(2)]
    public string Comment { get; set; } = null!;
}
