using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardStatisticsViewComponents;

public class DashboardStatisticsWidgetViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.categoryCount = await context.Categories.CountAsync();

        ViewBag.maxProductPrice = await context.Products.MaxAsync(p => p.Price);
        ViewBag.minProductPrice = await context.Products.MinAsync(p => p.Price);

        //ViewBag.maxProductPriceName = context.Products.OrderByDescending(p => p.Price).Select(p => p.Name).FirstOrDefault();
        //decimal maxPrice = await context.Products.MaxAsync(p => p.Price);

        //ViewBag.maxProductPriceName = await context.Products
        //    .Where(p => p.Price == maxPrice)
        //    .Select(p => p.Name)
        //    .FirstOrDefaultAsync();
        ViewBag.maxProductPriceName = context.Products.Where(p => p.Price == context.Products.Max(p => p.Price)).Select(p => p.Name).FirstOrDefault();
        //ViewBag.minProductPriceName = context.Products.OrderBy(p => p.Price).Select(p => p.Name).FirstOrDefault();
        ViewBag.minProductPriceName = context.Products.Where(p => p.Price == context.Products.Min(p => p.Price)).Select(p => p.Name).FirstOrDefault();

        ViewBag.sumProductStock = await context.Products.SumAsync(p => p.Stock);
        ViewBag.averageProductStock = await context.Products.AverageAsync(p => p.Stock);
        ViewBag.averageProductPrice = await context.Products.AverageAsync(p => p.Price);

        //ViewBag.priceGreaterThan500Product = context.Products.Count(p => p.Price > 500);
        ViewBag.priceGreaterThan1000Product = await context.Products.Where(p => p.Price > 1000).CountAsync();
        ViewBag.getIdIs4ProductName = await context.Products.Where(p => p.Id == 4).Select(p => p.Name).FirstOrDefaultAsync();
        ViewBag.stockCountGreaterThan50AndLessThan100ProductCount = await context.Products.Where(p => p.Stock > 50 && p.Stock < 100).CountAsync();

        return View();
    }
}