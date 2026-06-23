using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardIndexChartComponents;

public class DashboardIndexOrderStatusChartViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var orders = await context.Orders
            .GroupBy(o => o.Status)
            .Select(g => new OrderStatusChartViewModel
            {
                Status = g.Key,
                Count = g.Count()
            }).ToListAsync();
        
        return View(orders);
    }
}