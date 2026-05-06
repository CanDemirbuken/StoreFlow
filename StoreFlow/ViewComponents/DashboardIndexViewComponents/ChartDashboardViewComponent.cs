using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class ChartDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}