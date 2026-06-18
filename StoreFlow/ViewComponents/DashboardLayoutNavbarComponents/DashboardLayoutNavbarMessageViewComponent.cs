using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardLayoutNavbarComponents;

public class DashboardLayoutNavbarMessageViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var messages = await context.Messages.Where(x => x.IsRead == false).OrderByDescending(x => x.Id).Take(3).ToListAsync();
        ViewBag.MessageCount = messages.Where(x => x.IsRead == false).Count();

        return View(messages);
    }
}
