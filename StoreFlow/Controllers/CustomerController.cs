using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers;

public class CustomerController(StoreDbContext context) : Controller
{
    public async ValueTask<IActionResult> CustomerListOrderByName()
    {
        var customers = await context.Customers.OrderBy(c => c.Name).ThenBy(c => c.Surname).ToListAsync();
        return View(customers);
    }

    public async ValueTask<IActionResult> CustomerListOrderByDescBalance()
    {
        var customers = await context.Customers.OrderByDescending(c => c.Balance).ToListAsync();
        return View(customers);
    }

    public async ValueTask<IActionResult> CustomerGetByCity(string city)
    {
        var exist = await context.Customers.AnyAsync(c => c.City == city);
        ViewBag.city = city;

        if (city != null)
        {
            if (exist)
                ViewBag.exist = "true";
            else
                ViewBag.exist = "false";
        }

        return View();
    }

    [HttpGet]
    public async ValueTask<IActionResult> CreateCustomer()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateCustomer(Customer customer)
    {
        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(CustomerListOrderByName));
    }

    [HttpGet]
    public async ValueTask<IActionResult> UpdateCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        return View(customer);
    }

    [HttpPost]
    public async ValueTask<IActionResult> UpdateCustomer(Customer customer)
    {
        context.Customers.Update(customer);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(CustomerListOrderByName));
    }

    public async ValueTask<IActionResult> DeleteCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        context.Customers.Remove(customer);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(CustomerListOrderByName));
    }
}