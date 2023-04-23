namespace WebAppFrontend.Models;

public class ShowcaseModel
{
    public string Title { get; set; } = null!;
    public string Details { get; set; } = null!;
    public string? Other { get; set; }
    public string ButtonText { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
