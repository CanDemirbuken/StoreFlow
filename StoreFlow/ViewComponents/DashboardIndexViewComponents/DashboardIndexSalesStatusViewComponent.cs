using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.DashboardIndexViewComponents;

public class DashboardIndexSalesStatusViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}