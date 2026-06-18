using StoreFlow.Entities;

namespace StoreFlow.Models;

public class CustomerOrderViewMOdel
{
    public string CustomerName { get; set; }
    public List<Order> Orders { get; set; }
}
