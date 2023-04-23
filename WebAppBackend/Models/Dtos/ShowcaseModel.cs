using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Models.Dtos;

public class ShowcaseModel
{
    [MinLength(2)]
    public string Title { get; set; } = null!;

    [MinLength(2)]
    public string Details { get; set; } = null!;

    [MinLength(2)]
    public string? Other { get; set; }

    [MinLength(2)]
    public string ButtonText { get; set; } = null!;

    [MinLength(2)]
    public string ImageUrl { get; set; } = null!;

    public static implicit operator ShowcaseEntity(ShowcaseModel model)
    {
        return new ShowcaseEntity
        {
            Title = model.Title,
            Details = model.Details,
            Other = model.Other,
            ButtonText = model.ButtonText,
            ImageUrl = model.ImageUrl
        };
    }
}
