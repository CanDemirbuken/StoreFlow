using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexDailySalesViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}