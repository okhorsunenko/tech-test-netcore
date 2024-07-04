using System.Threading.Tasks;
using Todo.Models.TodoItems;

namespace Todo.Interfaces
{
    public interface ITodoItemService
    {
        TodoItemEditFields GetTodoItemEditFields(int todoItemId);
        Task<int> UpdateTodoItem(TodoItemEditFields fields);
    }
}
