using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Models.Entites;

public class UserEntity
{
    [Key, ForeignKey(nameof(User))]
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string RoleName { get; set; } = "product manager";
    public IdentityUser User { get; set; } = null!;

    public static implicit operator UserProfileModel(UserEntity entity)
    {
        return new UserProfileModel
        {
            Name = $"{entity.FirstName} {entity.LastName}",
            RoleName = entity.RoleName,
            Email = entity.Email
        };
    }

    public static implicit operator UserEntity(IdentityUser user)
    {
        return new UserEntity
        {
            Id = user.Id,
            Email = user.Email!
        };
    }

}
