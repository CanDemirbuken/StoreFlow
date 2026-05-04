using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents;

public class HeadDashboardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}