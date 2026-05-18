using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers;

public class ProductController(StoreDbContext context) : Controller
{
    public async ValueTask<IActionResult> ProductList()
    {
        var products = await context.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    [HttpGet]
    public async ValueTask<IActionResult> CreateProduct()
    {
        var categories = context.Categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();

        ViewBag.Categories = categories;

        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateProduct(Entities.Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(ProductList));
    }

    [HttpGet]
    public async ValueTask<IActionResult> UpdateProduct(int id)
    {
        var categories = await context.Categories.ToListAsync();
        var product = await context.Products.FindAsync(id);

        ViewBag.Categories = new SelectList(
            categories,
            "Id",
            "Name",
            product.CategoryId
        );

        return View(product);
    }

    [HttpPost]
    public async ValueTask<IActionResult> UpdateProduct(Entities.Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(ProductList));
    }

    public async ValueTask<IActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(ProductList));
    }

    public async ValueTask<IActionResult> FirstFiveProductList()
    {
        var products = await context.Products.Include(p => p.Category).Take(5).ToListAsync();
        return View(products);
    }

    public async ValueTask<IActionResult> LastFiveProductList()
    {
        var products = await context.Products.Include(p => p.Category).OrderByDescending(p => p.Id).Take(5).ToListAsync();
        return View(products);
    }

    public async ValueTask<IActionResult> SkipFourProductList()
    {
        var products = await context.Products.Include(p => p.Category).Skip(4).Take(10).ToListAsync();
        return View(products);
    }
}
