using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.DTOs;
using StoreFlow.Entities;

namespace StoreFlow.Controllers;

public class TodoController(StoreDbContext context) : Controller
{

    public async ValueTask<IActionResult> TodoList()
    {
        var todos = await context.Todos.ToListAsync();
        return View(todos);
    }

    // Bu metod, veritabanına önceden tanımlanmış birkaç "Todo" öğesi eklemek için kullanılabilir.
    [HttpGet]
    public async ValueTask<IActionResult> CreateTodo()
    {
        var todos = new List<Todo>
        {
            new Todo {Description = "Mail gönder", Status = true , Priority = "Birincil"},
            new Todo {Description = "Rapor hazırla", Status = true, Priority = "İkincil"},
            new Todo {Description = "Toplantıya katıl", Status = true, Priority = "Birincil"},
        };

        await context.Todos.AddRangeAsync(todos);
        await context.SaveChangesAsync();

        return View();
    }

    public async ValueTask<IActionResult> TodoAggregate()
    {
        var priorityFirstly = await context.Todos.Where(x => x.Priority == "Birincil").Select(y => y.Description).ToListAsync();

        string result = priorityFirstly.Aggregate((acc, desc) => acc + ", " + desc);
        ViewBag.Result = result;

        return View();
    }

    public async ValueTask<IActionResult> TodoAppend()
    {
        var todos = await context.Todos.Where(x => !x.Status).Select(y => y.Description).ToListAsync();
        var result = todos.Append("Gün sonunda tüm görevleri kontrol etmeyi unutmayın!").ToList();

        return View(result);
    }

    public async ValueTask<IActionResult> TodoPrepend()
    {
        var todos = await context.Todos.Where(x => !x.Status).Select(y => y.Description).ToListAsync();
        var result = todos.Prepend("Gün başında yapılacaklar listesi:").ToList();

        return View(result);
    }

    public async ValueTask<IActionResult> TodoChunk()
    {
        var todos = await context.Todos.Where(x => !x.Status).ToListAsync();
        var result = todos.Chunk(2).ToList();

        return View(result);
    }

    public async ValueTask<IActionResult> TodoConcat()
    {
        var todosFirst = await context.Todos.Where(x => x.Priority == "Birincil").ToListAsync();
        var todosSecond = await context.Todos.Where(x => x.Priority == "İkincil").ToListAsync();

        var result = todosFirst.Concat(todosSecond).ToList();

        return View(result);
    }

    public async ValueTask<IActionResult> TodoUnionBy()
    {
        var todosFirst = await context.Todos.Where(x => x.Priority == "Birincil").ToListAsync();
        var todosSecond = await context.Todos.Where(x => x.Priority == "İkincil").ToListAsync();

        var result = todosFirst.UnionBy(todosSecond, todo => todo.Description).ToList();

        return View(result);
    }

    #region AJAX

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

    #endregion AJAX
}