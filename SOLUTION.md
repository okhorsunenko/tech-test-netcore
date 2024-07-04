# Storyteller Technical Test – A Todo-List Project

### General info
Oleg Khorsunenko

Implementation time: Totaly it takes about 4 hours to do all the tasks.
3:30 on main tasks and 30 minutes on refactoring, clean up, error handling.
I have separated that done part of it on one day(3 July) and second bunch on the next day(4 July).

After finish main tasks I decided to made a little refactor, add error handling and adding unit tests.
Also, created `TodoItemService` which incapsulated logic for edit Todo Item.

Couple of improvements which could be done later:
- Services could be done for other controllers as well, like for `TodoListController`.
- Better error handling, cover more issues and return specific error page with better view.
- Adding loging to the project.

Please find below my comments regarding task implementation

# Task 1
Forked the project, create couple of users, play with it
Also added changes to make migration automatically. Changes was done in Startup.cs

# Task 2
As task was required to make changes exactly in `Views/TodoList/Detail.cshtml`I have made changes in this file, adding OrderBy `Importance` to Items.
Also, it could be done in the backend.

# Task 3
Just fix unit test.

# Task 4
Using attribute `[Display(Name = "")]` to display correct name.

# Task 5
This task could be resolved in two ways:
 1. hide items on the backend and return list with only not done items
 2. hide items on the ui using JS code
I chose option 2, as it allow more flexibility to show/hide done items.

Option 1 will require changes in `TodoListController` like:
```
public IActionResult Detail(int todoListId, bool hideDone = false)
{
    // previous code
    if(hideDone)
        viewmodel.Items = viewmodel.Items.Where(i => i.IsDone == !hideDone).ToList();
    return View(viewmodel);
}    
```

# Task 6
Updated query method

# Task 7
Here want only mention that makes ordering by rank in the backend.
When it applied on UI, it show ranking inside importance. Like first showed high importance and inside these items ordered by rank. Same for other importance options.

# Task 8
When I start working on this task, I checked Gravatar documentation and found that they provide different ways to implement their functionality. 
I thinking between using REST API and JSON endpoints. After some thoughts I have decided create this functionality using JSON endpoints and do it on UI. 
As I see couple of options to implement it, I also think about trade-offs:
1. Implement on UI using JS call to Gravatar service
    - Pros: Up to date data, as we get every time actual information.  
    - Cons: will be many requests to Gravatar service
2. Implement on backend and save info to `UserSummaryViewmodel`
    - Pros: Call Gravatar service only one time and save this data. Less calls when open app every time.
    - Cons: Data(name and image) could be out of date. Could not be used for logged user, here mean information in top right info about user profile.
3. Implement on backend, using Gravatar service(or create new one):
    - Pros: Better Error Handling, could be better async. Up to date data.
    - Cons: Little bit complicated for show profile name on the top right compared with JS code. Maybe will require create custom component.

Depend on this information I decided use option 1 and implement it on UI using JS.

# Task 9
Adding API controller(`api/TodoItemsController`) to project and form to make create request.
After sending request and if it successful page reloaded.
Removed unnecessary method from `TodoItemsController` and remove not used page - `Views/TodoItem/Create.cshtml`

# Task 10
Add API call to `api/TodoItemsController` which responsible for updating rank of item. 