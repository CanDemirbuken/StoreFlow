using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents;

public class ThemeSettingsWrapperDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}