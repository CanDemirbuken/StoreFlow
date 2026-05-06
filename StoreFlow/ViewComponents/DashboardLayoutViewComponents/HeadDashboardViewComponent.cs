using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardLayoutViewComponents;

public class HeadDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}