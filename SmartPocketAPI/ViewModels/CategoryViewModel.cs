namespace SmartPocketAPI.ViewModels;

public class CategoryViewModel
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameSpanish { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionSpanish { get; set; } = string.Empty;
}