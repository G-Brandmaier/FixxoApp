using WebAppBackend.Models.Entites;

namespace WebAppBackend.Models.Dtos;

public class TagModel
{
    public string Name { get; set; } = null!;

    public static implicit operator TagEntity(TagModel model)
    {
        return new TagEntity
        {
            Name = model.Name

        };
    }
}
