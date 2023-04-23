namespace WebAppFrontend.Models;

public class TagCollectionModel
{
    public string TagName { get; set; } = null!;
    public List<ProductModel> Products { get; set; } = null!;
}
