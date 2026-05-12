namespace StoreFlow.Extensions;

public static class OrderStatusExtensions
{
    public static string GetBadgeClass(this string? status)
    {
        return status switch
        {
            "Taşıma Durumunda" => "badge-warning",
            "İptal Edildi" => "badge-danger",
            "Teslim Edildi" => "badge-success",
            "Sipariş Alındı" => "badge-primary",
            "Ödeme Bekleniyor" => "badge-info",
            _ => "badge-secondary"
        };
    }
}