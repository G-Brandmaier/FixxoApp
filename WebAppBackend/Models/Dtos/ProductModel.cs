using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Models.Dtos;

public class ProductModel
{
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string TagName { get; set; } = null!;
    public int? StarRating { get; set; }
    public string ImageUrl { get; set; } = null!;


    public static implicit operator ProductEntity(ProductModel model)
    {
        return new ProductEntity
        {
            ArticleNumber = model.ArticleNumber,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            StarRating = model.StarRating,
            ImageUrl = model.ImageUrl               
        };
    }
}