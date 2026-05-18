using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.Controllers
{
    public class OrderController(StoreDbContext context) : Controller
    {
        public async ValueTask<IActionResult> AllOrderCountSmallerThanFive()
        {
            bool orderCount = await context.Orders.AllAsync(o => o.Count <= 5);
            if (orderCount)
                ViewBag.v = "Tüm siparişler 5 adetten küçüktür.";
            else
                ViewBag.v = "Tüm siparişler 5 adetten küçük değildir.";
            return View();
        }
    }
}
