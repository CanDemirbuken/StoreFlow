using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class SalesDataDashboardViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var orders = await context.Orders.Include(c => c.Customer).Include(p => p.Product).OrderByDescending(o => o.Id).Take(5).ToListAsync();

        return View(orders);
    }
}