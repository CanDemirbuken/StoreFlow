using StoreFlow.Extensions;

namespace StoreFlow.Models;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Status { get; set; }
    public string CategoryBadgeClass => Status.GetCategoryBadgeClass();
    public string StatusText => Status.GetStatusText();
}
