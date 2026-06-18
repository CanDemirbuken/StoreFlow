using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

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

    public async ValueTask<IActionResult> CustomerListByCity()
    {
        var customers = await context.Customers.ToListAsync();
        var groupedCustomers = customers.GroupBy(c => c.City).ToList();

        return View(groupedCustomers);
    }

    public async ValueTask<IActionResult> CustomersCountByCity()
    {
        var query = from c in context.Customers
                    group c by c.City into cityGroup
                    select new CustomerCityGroup
                    {
                        City = cityGroup.Key,
                        CustomerCount = cityGroup.Count()
                    };

        var model = await query.OrderByDescending(c => c.CustomerCount).ToListAsync();

        return View(model);
    }

    public async ValueTask<IActionResult> CustomersDistinctCity()
    {
        var cities = await context.Customers.Select(c => c.City).Distinct().ToListAsync();
        return View(cities);
    }

    public async ValueTask<IActionResult> ParallelCustomers()
    {
        var customers = await context.Customers.ToListAsync();
        var parallelCustomers = customers.AsParallel().Where(c => c.City.StartsWith("A", StringComparison.OrdinalIgnoreCase)).ToList();

        return View(parallelCustomers);
    }

    public async ValueTask<IActionResult> ExceptByCustomers()
    {
        //var customers = await context.Customers.ToListAsync();
        //var customerListInIstanbul = await context.Customers.Where(x => x.City == "Istanbul").ToListAsync();
        //var customersExceptByIstanbul = customers.Except(customerListInIstanbul).ToList();

        var customers = await context.Customers.ToListAsync();
        var customerListInIstanbul = await context.Customers
                                        .Where(x => x.City == "Istanbul")
                                        .Select(c => c.City)
                                        .ToListAsync();

        var customersExceptByIstanbul = customers.ExceptBy(customerListInIstanbul, c => c.City).ToList();

        return View(customersExceptByIstanbul);
    }

    public async ValueTask<IActionResult> CustomerListWithDefaultIfEmpty()
    {
        var customers = await context.Customers.Where(x => x.City == "Hakkari").ToListAsync();

        var customersDefaultIfEmpty = customers.DefaultIfEmpty(new Customer
        {
            Id = 0,
            Name = "Kayıt Yok",
            Surname = "-",
            City = "Ankara"
        }).ToList();

        return View(customersDefaultIfEmpty);
    }

    public async ValueTask<IActionResult> CustomerIntersectByCity()
    {
        var cityList1 = await context.Customers.Where(x => x.City == "Istanbul").Select(y => y.Name + " " + y.Surname).ToListAsync();
        var cityList2 = await context.Customers.Where(x => x.City == "Ankara").Select(y => y.Name + " " + y.Surname).ToListAsync();

        var intersectByCity = cityList1.Intersect(cityList2).ToList();

        return View(intersectByCity);
    }

    public async ValueTask<IActionResult> CustomerCastExample()
    {
        var customers = await context.Customers.ToListAsync();
        ViewBag.customers = customers;

        return View();
    }
}