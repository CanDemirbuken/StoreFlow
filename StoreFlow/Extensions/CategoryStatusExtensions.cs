namespace StoreFlow.Extensions;

public static class CategoryStatusExtensions
{
    public static string GetCategoryBadgeClass(this string? status)
    {
        return status switch
        {
            "True" => "badge-success",
            "False" => "badge-secondary",
            _ => "badge-secondary"
        };
    }

    public static string GetStatusText(this string? status)
    {
        return status switch
        {
            "True" => "Aktif",
            "False" => "Pasif",
            _ => "Pasif"
        };
    }
}