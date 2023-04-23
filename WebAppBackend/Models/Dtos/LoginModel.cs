using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAppBackend.Models.Dtos;

public class LoginModel
{
    [Required(ErrorMessage = "An email is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A password is required")]
    public string Password { get; set; } = null!;


    public static implicit operator IdentityUser(LoginModel model)
    {
        return new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };
    }
}
