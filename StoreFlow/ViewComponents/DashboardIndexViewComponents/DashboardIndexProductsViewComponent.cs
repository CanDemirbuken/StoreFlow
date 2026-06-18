using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents
{
    public class DashboardIndexProductsViewComponent(StoreDbContext context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await context.Products.OrderBy(p => p.Id).ToListAsync();

            var skippedLastProducts = products
                .SkipLast(5).ToList()
                .TakeLast(5).ToList();

            return View(skippedLastProducts);
        }
    }
}
