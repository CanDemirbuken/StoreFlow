using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexDailySalesViewComponent(StoreDbContext context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var todoPriorityChartData = await context.Todos
            .GroupBy(t => t.Priority)
            .Select(g => new Models.TodoPriorityChartViewModel
            {
                Status = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();

        return View(todoPriorityChartData);
    }
}