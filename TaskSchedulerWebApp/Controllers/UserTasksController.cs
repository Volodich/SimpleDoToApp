using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskSchedulerWebApp.Models;
using TaskSchedulerWebApp.Models.Repositories.Abstract;
using TaskSchedulerWebApp.Models.ViewModels;
using TaskSchedulerWebApp.Settings;

namespace TaskSchedulerWebApp.Controllers
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private IUserTaskRepositories TaskRepositories { get; }
        private readonly UserManager<User> _userManager;

        public UserTasksController(ProcrastinatorContext context, IUserTaskRepositories taskRepositories, UserManager<User> userManager)
        {
            TaskRepositories = taskRepositories;
            _userManager = userManager;

            Config.SidebarVisible = false;
            Config.UseBootstrap = false;
        }

        // GET: UserTasks
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Задачи";

            var user = HttpContext.User.Identity?.Name;
            var tasks = await TaskRepositories.GetUserTasksAsync(user);
            return View(tasks);
        }

        // GET: UserTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTask = await TaskRepositories.GetTaskByIdAsync(id.Value);

            if (userTask == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Подробнее";
            return View(userTask);
        }

        // GET: UserTasks/Create
        public async Task<IActionResult> Create()
        {
            var taskLevels = await TaskRepositories.GetTaskLevelsName();
            SelectList levelsList = new SelectList(taskLevels, taskLevels[0]);

            var taskStates = await TaskRepositories.GetTaskStatesName();
            SelectList stateList = new SelectList(taskStates, taskStates[0]);

            ViewBag.taskLevels = levelsList;
            ViewBag.taskStates = stateList;

            ViewData["Title"] = "Добавить задачу";

            return View();
        }

        // POST: UserTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserTasksViewModel userTask)
        {
            ViewData["Title"] = "Добавить задачу";

            if (ModelState.IsValid)
            {
                await TaskRepositories.AddTaskAsync(HttpContext.User.Identity?.Name, (UserTask)userTask);

                return RedirectToAction(nameof(Index));
            }
            return View(userTask);
        }

        // GET: UserTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTask = await TaskRepositories.GetTaskByIdAsync(id.Value);
            if (userTask == null)
            {
                return NotFound();
            }

            var taskLevels = await TaskRepositories.GetTaskLevelsName();
            SelectList levelsList = new SelectList(taskLevels, userTask.TaskLevel.Name);

            var taskStates = await TaskRepositories.GetTaskStatesName();
            SelectList stateList = new SelectList(taskStates, userTask.TaskState.Name);

            ViewBag.taskLevels = levelsList;
            ViewBag.taskStates = stateList;

            ViewData["Title"] = "Изменить задачу";
            return View(userTask);
        }

        // POST: UserTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserTasksViewModel userTask)
        {
            if (id != userTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await TaskRepositories.UpdateTaskInformationAsync(userTask);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TaskRepositories.UserTaskExists(userTask.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Изменить задачу";
            return View(userTask);
        }

        // GET: UserTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTask = await TaskRepositories.GetTaskByIdAsync(id.Value);
            if (userTask == null)
            {
                return NotFound();
            }

            var taskLevels = await TaskRepositories.GetTaskLevelsName();
            SelectList levelsList = new SelectList(taskLevels, taskLevels[0]);

            var taskStates = await TaskRepositories.GetTaskStatesName();
            SelectList stateList = new SelectList(taskStates, taskStates[0]);

            ViewBag.taskLevels = levelsList;
            ViewBag.taskStates = stateList;

            ViewData["Title"] = "Удаление задачи";
            return View(userTask);
        }

        // POST: UserTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await TaskRepositories.RemoveTaskAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
