using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.DTOs;
using StoreFlow.Entities;

namespace StoreFlow.Controllers;

public class TodoController(StoreDbContext context) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Description))
            return BadRequest(new { success = false, message = "Açıklama boş olamaz." });

        var todo = new Todo
        {
            Description = request.Description,
            Status = false
        };

        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();

        return Ok(new { success = true, id = todo.Id, message = "Todo created successfully.", description = todo.Description, status = todo.Status });
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoStatusRequest request)
    {
        var todo = await context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Todo not found."
            });
        }

        todo.Status = request.Status;

        await context.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "Todo status updated successfully.",
            description = todo.Description,
            status = todo.Status
        });
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(int id)
    {
        var todo = await context.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Todo not found."
            });
        }
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
        return Ok(new
        {
            success = true,
            message = "Todo deleted successfully."
        });
    }
}