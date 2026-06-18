using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexInboxViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var messages = await context.Messages
                                .OrderBy(x => x.Id)
                                .ToListAsync();

        var lastFiveMessages = messages.TakeLast(5).ToList();

        return View(lastFiveMessages);
    }
}