using WebAppFrontend.Models;

namespace WebAppFrontend.ViewModels;

public class HomeIndexViewModel
{
    public ShowcaseModel Showcase { get; set; } = null!;
    public List<TagCollectionModel> TagCollection { get; set; } = new List<TagCollectionModel>();
}
