using StoreFlow.Entities;
using StoreFlow.Extensions;

namespace StoreFlow.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public string? Status { get; set; }
        public string StatusBadgeClass => Status.GetBadgeClass();
    }
}
