using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Data;
using Todo.EntityModelMappers.TodoItems;
using Todo.Services;
using Todo.Models.TodoItems;

namespace Todo.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TodoItemsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var todoItem = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance);

            await context.AddAsync(todoItem);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.TodoItemId }, todoItem);
        }
    }
}
