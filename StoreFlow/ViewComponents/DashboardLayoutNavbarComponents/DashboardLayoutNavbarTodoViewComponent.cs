using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardLayoutNavbarComponents;

public class DashboardLayoutNavbarTodoViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var todos = await context.Todos.Where(t => t.Status == false).OrderByDescending(t => t.Id).Take(5).ToListAsync();
        ViewBag.totalTodoCount = context.Todos.Count();

        return View(todos);
    }
}