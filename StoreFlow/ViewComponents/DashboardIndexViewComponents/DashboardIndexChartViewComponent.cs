using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexChartViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}