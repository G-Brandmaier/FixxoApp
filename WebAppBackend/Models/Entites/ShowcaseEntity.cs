using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Models.Entites;

public class ShowcaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Details { get; set; } = null!;
    public string? Other { get; set; }
    public string ButtonText { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;


    public static implicit operator ShowcaseModel(ShowcaseEntity entity)
    {
        return new ShowcaseModel
        {
            Title = entity.Title,
            Details = entity.Details,
            Other = entity.Other,
            ButtonText = entity.ButtonText,
            ImageUrl = entity.ImageUrl
        };
    }
}
