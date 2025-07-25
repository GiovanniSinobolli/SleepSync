using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SleepSync.Data;
using SleepSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SleepSync.Controllers
{
    [Authorize]
    public class MoodInsightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoodInsightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MoodInsights
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Set<MoodInsight>().Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MoodInsights/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodInsight = await _context.Set<MoodInsight>()
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MoodInsightId == id);
            if (moodInsight == null)
            {
                return NotFound();
            }

            return View(moodInsight);
        }

        // GET: MoodInsights/Create
        [Authorize(Roles = "User,Administrator")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: MoodInsights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoodInsightId,Message,CreatedAt,UserId")] MoodInsight moodInsight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moodInsight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", moodInsight.UserId);
            return View(moodInsight);
        }

        // GET: MoodInsights/Edit/5
        [Authorize(Roles = "User,Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodInsight = await _context.Set<MoodInsight>().FindAsync(id);
            if (moodInsight == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", moodInsight.UserId);
            return View(moodInsight);
        }

        // POST: MoodInsights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoodInsightId,Message,CreatedAt,UserId")] MoodInsight moodInsight)
        {
            if (id != moodInsight.MoodInsightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moodInsight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodInsightExists(moodInsight.MoodInsightId))
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
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", moodInsight.UserId);
            return View(moodInsight);
        }

        // GET: MoodInsights/Delete/5
        [Authorize(Roles = "User,Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodInsight = await _context.Set<MoodInsight>()
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MoodInsightId == id);
            if (moodInsight == null)
            {
                return NotFound();
            }

            return View(moodInsight);
        }

        // POST: MoodInsights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moodInsight = await _context.Set<MoodInsight>().FindAsync(id);
            if (moodInsight != null)
            {
                _context.Set<MoodInsight>().Remove(moodInsight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoodInsightExists(int id)
        {
            return _context.Set<MoodInsight>().Any(e => e.MoodInsightId == id);
        }
    }
}
