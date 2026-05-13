using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexActivityViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var activities = await context.Activities
            .ToListAsync();

        return View(activities);
    }
}