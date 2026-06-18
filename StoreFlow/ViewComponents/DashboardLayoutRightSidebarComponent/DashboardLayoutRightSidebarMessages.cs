using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardLayoutRightSidebarComponent;

public class DashboardLayoutRightSidebarMessages(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var messages = await context.Messages
            .Where(x => x.IsRead == false)
            .OrderByDescending(m => m.Id)
            .Take(5)
            .ToListAsync();

        return View(messages);
    }
}
