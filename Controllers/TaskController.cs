using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SleepSync.Data;
using SleepSync.Models.Entities;
using SleepSync.Models.Viewmodels;
using System.Security.Claims;

namespace SleepSync.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        //injecting userManager to get userId from the identity Login system 
        public TaskController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        //post method to add a task to the database
        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewmodel viewmodel)
        {
            var task = new UserTask
            {

                UserId = _userManager.GetUserId(User),
                Title = viewmodel.Title,
                Description = viewmodel.Description,
                Priority = viewmodel.Priority,
                IsCompleted = viewmodel.IsCompleted,
                Mood = viewmodel.Mood
            };

            await _context.UserTasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return View();
        }

        //get method to display all tasks for the logged-in user
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var userTaskslist = await _context.UserTasks.ToListAsync();

            return View(userTaskslist);
        }

        //edit method to edit the tasks
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editTask = await _context.UserTasks.FindAsync(id);

            return View(editTask);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserTask viewmodel)
        {
            var task = await _context.UserTasks.FindAsync(viewmodel.Id);

            if (task is not null)
            {
                task.Title = viewmodel.Title;
                task.Description = viewmodel.Description;
                task.Priority = viewmodel.Priority;
                task.IsCompleted = viewmodel.IsCompleted;
                task.Mood = viewmodel.Mood;
                task.Mood = viewmodel.Mood;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        //delete method to delete the tasks
        [HttpPost]
        public async Task<IActionResult> Delete(UserTask viewmodel)
        {
            var deleteTask = await _context.UserTasks.FirstOrDefaultAsync(x => x.Id == viewmodel.Id);

            if(deleteTask is not null)
            {
                _context.UserTasks.Remove(deleteTask);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
