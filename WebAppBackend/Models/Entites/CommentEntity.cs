using System.ComponentModel.DataAnnotations;

namespace WebAppBackend.Models.Entites;

public class CommentEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Comment { get; set; } = null!;
}
