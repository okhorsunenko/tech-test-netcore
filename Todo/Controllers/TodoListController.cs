using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoLists;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(ApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            try
            {
                var userId = User.Id();
                var todoLists = dbContext.RelevantTodoLists(userId);
                var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
                return View(viewmodel);
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong. Error: " + ex.Message);
            }
        }

        public IActionResult Detail(int todoListId, bool orderByRank = false)
        {
            try
            {
                var todoList = dbContext.SingleTodoList(todoListId);

                if (todoList != null && orderByRank)
                    todoList.Items = todoList.Items.OrderBy(ti => ti.Rank).ToList();

                var viewmodel = TodoListDetailViewmodelFactory.Create(todoList);
                return View(viewmodel);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. Error: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            try
            {
                var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

                var todoList = new TodoList(currentUser, fields.Title);

                await dbContext.AddAsync(todoList);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Create", "TodoItem", new { todoList.TodoListId });
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. Error: " + ex.Message);
            }
        }
    }
}