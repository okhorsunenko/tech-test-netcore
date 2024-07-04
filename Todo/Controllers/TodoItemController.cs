using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.EntityModelMappers.TodoItems;
using Todo.Interfaces;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        private readonly ITodoItemService todoItemService;

        public TodoItemController(ApplicationDbContext dbContext, ITodoItemService todoItemService)
        {
            this.todoItemService = todoItemService;
        }

        [HttpGet]
        public IActionResult Edit(int todoItemId)
        {
            try
            {
                var fields = todoItemService.GetTodoItemEditFields(todoItemId);
                return View(fields);
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong. Error: " + ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoItemEditFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }
            
            try
            {
                var todoListId = await todoItemService.UpdateTodoItem(fields);

                return RedirectToListDetail(todoListId);
            }
            catch(Exception ex)
            {
               return BadRequest("Something went wrong. Error: " + ex.Message);
            }
        }

        private RedirectToActionResult RedirectToListDetail(int fieldsTodoListId)
        {
            return RedirectToAction("Detail", "TodoList", new {todoListId = fieldsTodoListId});
        }
    }
}