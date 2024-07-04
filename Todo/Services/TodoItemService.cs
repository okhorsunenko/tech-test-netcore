using System.Threading.Tasks;
using Todo.Data;
using Todo.EntityModelMappers.TodoItems;
using Todo.Interfaces;
using Todo.Models.TodoItems;

namespace Todo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext dbContext;

        public TodoItemService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TodoItemEditFields GetTodoItemEditFields(int todoItemId)
        {
            var todoItem = dbContext.SingleTodoItem(todoItemId);
            return TodoItemEditFieldsFactory.Create(todoItem);
        }

        public async Task<int> UpdateTodoItem(TodoItemEditFields fields)
        {
            var todoItem = dbContext.SingleTodoItem(fields.TodoItemId);

            TodoItemEditFieldsFactory.Update(fields, todoItem);

            dbContext.Update(todoItem);
            await dbContext.SaveChangesAsync();

            return todoItem.TodoListId;
        }
    }
}
