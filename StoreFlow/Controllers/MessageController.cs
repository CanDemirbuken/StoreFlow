using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.Controllers;

public class MessageController(StoreDbContext context) : Controller
{
    public async ValueTask<IActionResult> MessageList()
    {
        var messages = await context.Messages.AsNoTracking().ToListAsync();
        return View(messages);
    }
}
