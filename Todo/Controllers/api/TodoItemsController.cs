using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Data;
using Todo.Services;
using Todo.Models.TodoItems;

namespace Todo.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TodoItemsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var todoItem = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance);

            await dbContext.AddAsync(todoItem);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.TodoItemId }, todoItem);
        }

        [HttpPost("{id}/rank")]
        public async Task<IActionResult> UpdateRank(int id, [FromBody] int newRank)
        {
            var todoItem = dbContext.SingleTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Rank = newRank;
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
