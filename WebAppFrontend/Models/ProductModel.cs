using System.ComponentModel.DataAnnotations;

namespace WebAppFrontend.Models;

public class ProductModel
{
    [Display(Name = "Article Number")]
    public string ArticleNumber { get; set; } = null!;

    [Display(Name = "Product Name")]
    public string Name { get; set; } = null!;

    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    [Display(Name = "Tag Name")]
    public string TagName { get; set; } = null!;

    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Display(Name = "Star Rating")]
    public int StarRating { get; set; }

    [Display(Name = "Image Url")]
    public string ImageUrl { get; set; } = null!;
}
