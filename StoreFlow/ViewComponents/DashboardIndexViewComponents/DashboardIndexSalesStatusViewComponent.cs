using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexSalesStatusViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var sales = await context.Customers
            .GroupBy(c => c.City)
            .Select(g => new CustomerCityChartViewModel
            {
                City = g.Key,
                Count = g.Count()
            }).ToListAsync();

        return View(sales);
    }
}