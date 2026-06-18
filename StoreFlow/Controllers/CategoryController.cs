using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers;

public class CategoryController(StoreDbContext context) : Controller
{
    public async ValueTask<IActionResult> CategoryList()
    {
        var categories = await context.Categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Status = c.Status.ToString()
        }).ToListAsync();

        return View(categories);
    }

    [HttpGet]
    public async ValueTask<IActionResult> CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateCategory(Category category)
    {
        category.Status = false;
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(CategoryList));
    }

    [HttpGet]
    public async ValueTask<IActionResult> UpdateCategory(int id)
    {
        var category = await context.Categories.FindAsync(id);
        return View(category);
    }

    [HttpPost]
    public async ValueTask<IActionResult> UpdateCategory(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(CategoryList));
    }

    public async ValueTask<IActionResult> DeleteCategory(int id)
    {
        var category = await context.Categories.FindAsync(id);
        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(CategoryList));
    }

    public async ValueTask<IActionResult> ReverseCategory()
    {
        var categories = await context.Categories.OrderBy(x => x.Id).ToListAsync();
        categories.Reverse();

        return View(categories);
    }

    public async ValueTask<IActionResult> FirstCategory()
    {
        var firstCategory = await context.Categories.FirstAsync();
        ViewBag.firstCategory = firstCategory.Name;

        return View();
    }

    public async ValueTask<IActionResult> SingleOrDefaultCategory()
    {
        var singleCategory = await context.Categories.SingleOrDefaultAsync(x => x.Name == "Anne ve Bebek Ürünleri");
        ViewBag.singleCategory = singleCategory.Name;

        return View();
    }

}