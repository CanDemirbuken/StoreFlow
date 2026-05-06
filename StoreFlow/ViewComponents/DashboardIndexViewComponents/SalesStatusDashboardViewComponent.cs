using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class SalesStatusDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}