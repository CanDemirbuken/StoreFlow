using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardLayoutViewComponents;

public class SidebarDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}