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
    public class SleepController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> _userManager;

        public SleepController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSleepViewmodel viewmodel)
        {
            // Combine today's date with times from inputs
            var today = DateTime.Today;
            var bedTime = today.Add(viewmodel.BedTime.TimeOfDay);
            var wakeTime = today.Add(viewmodel.WakeTime.TimeOfDay);

            // Handle sleep past midnight
            if (wakeTime <= bedTime)
            {
                wakeTime = wakeTime.AddDays(1);
            }

            var hoursSlept = (int)Math.Round((wakeTime - bedTime).TotalHours);

            var sleeper = new SleepRecord
            {
                UserId = _userManager.GetUserId(User),
                BedTime = bedTime,
                WakeTime = wakeTime,
                SleepHours = hoursSlept,
                AutoMood = viewmodel.AutoMood,
                Notes = viewmodel.Notes,
                Date = viewmodel.Date
            };

            await dbContext.SleepRecords.AddAsync(sleeper);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sleepRecs = await dbContext.SleepRecords.ToListAsync();

            return View(sleepRecs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dorminhoco = await dbContext.SleepRecords.FindAsync(id);
            
            return View(dorminhoco);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SleepRecord sleep)
        {
            var dorminhoco = await dbContext.SleepRecords.FindAsync(sleep);

            if(dorminhoco is not null)
            {
                dorminhoco.BedTime = sleep.BedTime;
                dorminhoco.WakeTime = sleep.WakeTime;
                dorminhoco.SleepHours = sleep.SleepHours;
                dorminhoco.AutoMood = sleep.AutoMood;
                dorminhoco.Notes = sleep.Notes;
                dorminhoco.Date = sleep.Date;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SleepRecord sleep)
        {
            var deleteReport = await dbContext.SleepRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == sleep.Id);

            if (deleteReport is not null)
            {
                dbContext.SleepRecords.Remove(deleteReport);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
