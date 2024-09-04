using ControllerAPI.Database;
using ControllerAPI.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodoItems()
        //{
        //    return await _context.TodoItems
        //        .Select(x => ItemToDTO(x))
        //        .ToListAsync();
        //}

        //// GET: api/TodoItems/5
        //// <snippet_GetByID>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TodoDTO>> GetTodoItem(long id)
        //{
        //    var todoItem = await _context.TodoItems.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return ItemToDTO(todoItem);
        //}
        //// </snippet_GetByID>

        //// PUT: api/TodoItems/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //// <snippet_Update>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem(long id, TodoDTO todoDTO)
        //{
        //    if (id != todoDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var todoItem = await _context.TodoItems.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    todoItem.Name = todoDTO.Name;
        //    todoItem.IsComplete = todoDTO.IsComplete;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
        //// </snippet_Update>

        //// POST: api/TodoItems
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //// <snippet_Create>
        //[HttpPost]
        //public async Task<ActionResult<TodoDTO>> PostTodoItem(TodoDTO todoDTO)
        //{
        //    var todoItem = new Todo
        //    {
        //        IsComplete = todoDTO.IsComplete,
        //        Name = todoDTO.Name
        //    };

        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(
        //        nameof(GetTodoItem),
        //        new { id = todoItem.Id },
        //        ItemToDTO(todoItem));
        //}
        //// </snippet_Create>

        //// DELETE: api/TodoItems/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(long id)
        //{
        //    var todoItem = await _context.TodoItems.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TodoItems.Remove(todoItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TodoItemExists(long id)
        //{
        //    return _context.TodoItems.Any(e => e.Id == id);
        //}

        //private static TodoDTO ItemToDTO(Todo todoItem) =>
        //   new TodoDTO
        //   {
        //       Id = todoItem.Id,
        //       Name = todoItem.Name,
        //       IsComplete = todoItem.IsComplete
        //   };
    }
}
