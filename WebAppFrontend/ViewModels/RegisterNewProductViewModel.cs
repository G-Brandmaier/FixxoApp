using System.ComponentModel.DataAnnotations;

namespace WebAppFrontend.ViewModels;

public class RegisterNewProductViewModel
{
    [MinLength(2)]
    [Display(Name = "Article number")]
    public string ArticleNumber { get; set; } = null!;

    [MinLength(2)]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [MinLength(5)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [MinLength(2)]
    [Display(Name = "Tag name")]
    public string TagName { get; set; } = null!;

    [Display(Name = "Star rating")]
    public decimal? StarRating { get; set; }

    [Display(Name = "Image Url")]
    public string ImageUrl { get; set; } = null!;
}
