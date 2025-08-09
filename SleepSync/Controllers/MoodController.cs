using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SleepSync.Data;
using SleepSync.Models.Entities;

namespace SleepSync.Controllers
{
    [Authorize]
    public class MoodController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public MoodController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var latestSleep = await _dbContext.SleepRecords
                .Where(sr => sr.UserId == userId)
                .OrderByDescending(sr => sr.Date)
                .FirstOrDefaultAsync();

            if (latestSleep == null)
            {
                ViewBag.MoodStatus = "No sleep records found.";
                return View();
            }

            string moodMessage;
            string emoji;

            if (latestSleep.SleepHours >= 7)
            {
                moodMessage = "Good";
                emoji = "✅";
            }
            else if (latestSleep.SleepHours >= 4)
            {
                moodMessage = "Neutral";
                emoji = "⚪";
            }
            else
            {
                moodMessage = "Poor";
                emoji = "❌";
            }

            ViewBag.MoodStatus = $"Your mood is: {moodMessage} {emoji}";
            ViewBag.HoursSlept = latestSleep.SleepHours;

            return View();
        }
    }
}
