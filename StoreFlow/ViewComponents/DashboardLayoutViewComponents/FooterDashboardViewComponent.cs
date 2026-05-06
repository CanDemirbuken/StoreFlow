using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardLayoutViewComponents;

public class FooterDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}