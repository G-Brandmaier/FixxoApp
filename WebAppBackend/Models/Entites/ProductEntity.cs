using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Models.Entites;

public class ProductEntity
{
    [Key,ForeignKey(nameof(TagId))]
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public int TagId { get; set; }
    public TagEntity Tag { get; set; } = null!;
    public int? StarRating { get; set; }
    public string ImageUrl { get; set; } = null!;

    public static implicit operator ProductModel(ProductEntity entity)
    {
        return new ProductModel
        {
            ArticleNumber = entity.ArticleNumber,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            StarRating = entity.StarRating,
            TagName = entity.Tag.Name,
            ImageUrl = entity.ImageUrl
        };
    }

}
