using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Models.Dtos;

public class RegisterUserModel
{
    [Required(ErrorMessage = "A first name is required")]
    [MinLength(2)]
    [RegularExpression(@"^[a-öA-Ö]+(?:['´-][a-öA-Ö]+)*$", ErrorMessage = "Not a valid first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "A last name is required")]
    [MinLength(2)]
    [RegularExpression(@"^[a-öA-Ö]+(?:['´-][a-öA-Ö]+)*$", ErrorMessage = "Not a valid last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "An email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Not a valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A password is required")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Not a valid password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Confirmation of password is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; } = null!;

    public string? RoleName { get; set; } = "product manager";


    public static implicit operator IdentityUser(RegisterUserModel model)
    {
        return new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };
    }
    public static implicit operator UserEntity(RegisterUserModel model)
    {
        return new UserEntity
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            RoleName = model.RoleName!
        };
    }
}
