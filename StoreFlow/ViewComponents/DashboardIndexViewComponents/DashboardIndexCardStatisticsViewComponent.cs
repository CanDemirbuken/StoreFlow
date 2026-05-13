using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexCardStatisticsViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.TotalCustomerCount = await context.Customers.CountAsync();
        ViewBag.TotalCategoryCount = await context.Categories.CountAsync();
        ViewBag.TotalProductCount = await context.Products.CountAsync();
        ViewBag.AvgCustomerBalance = await context.Customers.AverageAsync(c => c.Balance);
        ViewBag.TotalOrderCount = await context.Orders.CountAsync();
        ViewBag.SumOrderProductCount = await context.Orders.SumAsync(o => o.Count);

        return View();
    }
}