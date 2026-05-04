using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents;

public class RightSidebarDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}