using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Models.Dtos;

public class CommentModel
{
    [MinLength(2)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "An email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Not a valid email")]
    public string Email { get; set; } = null!;

    [MinLength(5)]
    public string Comment { get; set; } = null!;

    public static implicit operator CommentEntity(CommentModel model)
    {
        return new CommentEntity
        {
            Name = model.Name,
            Email = model.Email,
            Comment = model.Comment
        };
    }
}
