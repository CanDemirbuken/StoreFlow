using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents
{
    public class TodoDashboardViewComponent(StoreDbContext context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var todos = await context.Todos.OrderByDescending(t => t.Id).Take(6).ToListAsync();
            return View(todos);
        }
    }
}
