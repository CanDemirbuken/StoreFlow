using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardLayoutViewComponents;

public class RightSidebarDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}