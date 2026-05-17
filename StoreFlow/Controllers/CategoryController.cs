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

}