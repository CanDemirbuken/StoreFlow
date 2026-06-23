using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardIndexChartComponents;

public class DashboardIndexOrderDateChartViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var orders = await context.Orders
            .GroupBy(o => o.Date.Date)
            .Select(g => new OrderDateChartViewModel
            {
                Date = g.Key.ToString("yyyy-MM-dd"),
                Count = g.Count()
            }).ToListAsync();

        return View(orders);
    }
}