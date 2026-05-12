using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class SalesDataDashboardViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var orders = await context.Orders
            .Include(c => c.Customer)
            .Include(p => p.Product)
            .OrderByDescending(o => o.Id)
            .Take(5)
            .Select(o => new OrderViewModel
            {
                Id = o.Id,
                Count = o.Count,
                Customer = o.Customer,
                Product = o.Product,
                Status = o.Status
            })
            .ToListAsync();

        return View(orders);
    }
}