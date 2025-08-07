using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SleepSync.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AccountController(
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpPost]
		public IActionResult ExternalLogin(string provider, string returnUrl = "/")
		{
			var redirectUrl = Url.Action(nameof(ExternalLoginCallback), new { returnUrl });
			var props = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
			return Challenge(props, provider);
		}

		[HttpGet]
		public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/", string? remoteError = null)
		{
			if (remoteError != null)
				return RedirectToAction("Login", "Account");

			var info = await _signInManager.GetExternalLoginInfoAsync();
			if (info == null)
				return RedirectToAction("Login", "Account");

			// Try to login using ExternalLoginSignIn
			var result = await _signInManager.ExternalLoginSignInAsync(
				info.LoginProvider, info.ProviderKey, isPersistent: false);

			if (result.Succeeded)
				return LocalRedirect(returnUrl);

			// Else create a local user
			var email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
			var user = new IdentityUser { UserName = email, Email = email };
			await _userManager.CreateAsync(user);
			await _userManager.AddLoginAsync(user, info);
			await _signInManager.SignInAsync(user, isPersistent: false);

			return LocalRedirect(returnUrl);
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = "/")
			=> View(model: returnUrl);
	}
}
