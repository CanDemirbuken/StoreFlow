using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardLayoutViewComponents;

public class ThemeSettingsWrapperDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}