using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SleepSync.Data;
using SleepSync.Models.Entities;
using SleepSync.Models.Viewmodels;

namespace SleepSync.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> userManager; // password hashing

        public UserController(ApplicationDbContext dbContext, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager; // handles password hashing
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewmodel viewmodel)
        {
            var user = new User
            {
                FirstName = viewmodel.FirstName,
                LastName = viewmodel.LastName,
                Email = viewmodel.Email,
                UserName = viewmodel.FirstName
            };
            var result = await userManager.CreateAsync(user, viewmodel.Password);
            
            if (result.Succeeded)
            {
                //User created with hashed password
                return RedirectToAction("Success");
            }
            else
            {
                // Handle errors (weak password, duplicate email, etc)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(viewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
