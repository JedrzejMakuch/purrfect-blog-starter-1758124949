using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Purrfect_Blog_Starter.Data;
using Purrfect_Blog_Starter.Models;
using Purrfect_Blog_Starter.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Purrfect_Blog_Starter.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        private IAuthenticationManager Auth => HttpContext.GetOwinContext().Authentication;

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            var isValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            await _userManager.ResetAccessFailedCountAsync(user.Id);

            var identity = await _userManager.CreateIdentityAsync(user, "ApplicationCookie");
            Auth.SignOut("ApplicationCookie");
            Auth.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Facts", "Cat");
        }

        [AllowAnonymous]
        public ActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = await _userManager.FindByNameAsync(model.UserName);
            if (existing != null)
            {
                ModelState.AddModelError("UserName", "User name is already taken.");
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e);
                return View(model);
            }

            var identity = await _userManager.CreateIdentityAsync(user, "ApplicationCookie");
            Auth.SignOut("ApplicationCookie");
            Auth.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            return RedirectToAction("Facts", "Cat");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Auth.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Cat");
        }
    }
}