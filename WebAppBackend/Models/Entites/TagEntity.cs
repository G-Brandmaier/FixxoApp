using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppBackend.Models.Entites;

public class TagEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

}
