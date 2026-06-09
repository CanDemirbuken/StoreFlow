using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using System.Xml.XPath;

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

        public async ValueTask<IActionResult> OrderListByStatus(string status = "")
        {
            var orders = await context.Orders.Where(o => o.Status.ToLower().Trim().Contains(status.ToLower().Trim())).ToListAsync();
            if (!orders.Any())
            {
                ViewBag.v = "Belirtilen durumda sipariş bulunamadı.";
            }

            return View(orders);
        }

        public async ValueTask<IActionResult> OrderListSearch(string name = "", string filterType = "")
        {
            if (filterType == "start")
            {
                var orders = await context.Orders.Where(o => o.Status.ToLower().Trim().StartsWith(name.ToLower().Trim())).ToListAsync();
                return View(orders);
            }
            else if (filterType == "end")
            {
                var orders = await context.Orders.Where(o => o.Status.ToLower().Trim().EndsWith(name.ToLower().Trim())).ToListAsync();
                return View(orders);
            }

            var orderList = await context.Orders.ToListAsync();
            return View(orderList);
        }

        public async ValueTask<IActionResult> OrderList()
        {
            var orders = await context.Orders.Include(x => x.Product).Include(y => y.Customer).ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async ValueTask<IActionResult> CreateOrder()
        {
            ViewBag.Products = await context.Products
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();

            ViewBag.Customers = await context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} {c.Surname}"
                })
                .ToListAsync();

            ViewBag.ProductPrices = await context.Products
                .Select(p => new
                {
                    id = p.Id,
                    price = p.Price
                })
                .ToListAsync();

            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateOrder(Order order)
        {
            var product = await context.Products.FindAsync(order.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Seçilen ürün bulunamadı.");
                await FillOrderViewBagAsync();
                return View(order);
            }

            order.UnitPrice = product.Price;
            order.TotalPrice = product.Price * order.Count;
            order.Date = DateTime.Now;
            order.Status ??= "Sipariş Alındı";

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderList));
        }

        public async ValueTask<IActionResult> DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(OrderList));
        }

        [HttpGet]
        public async ValueTask<IActionResult> UpdateOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await FillOrderViewBagAsync();

            return View(order);
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateOrder(Order order)
        {
            var existingOrder = await context.Orders.FindAsync(order.Id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            var product = await context.Products.FindAsync(order.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Seçilen ürün bulunamadı.");
                await FillOrderViewBagAsync();
                return View(order);
            }

            existingOrder.CustomerId = order.CustomerId;
            existingOrder.ProductId = order.ProductId;
            existingOrder.Count = order.Count;

            existingOrder.UnitPrice = product.Price;
            existingOrder.TotalPrice = product.Price * order.Count;

            existingOrder.Status = order.Status;
            existingOrder.Date = DateTime.Now;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderList));
        }

        private async Task FillOrderViewBagAsync()
        {
            ViewBag.Products = await context.Products
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();

            ViewBag.Customers = await context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} {c.Surname}"
                })
                .ToListAsync();

            ViewBag.ProductPrices = await context.Products
                .Select(p => new
                {
                    id = p.Id,
                    price = p.Price
                })
                .ToListAsync();
        }
    }
}