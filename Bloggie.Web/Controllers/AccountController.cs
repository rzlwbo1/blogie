using Bloggie.Web.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet, Route("/auth/register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost, ActionName("DoRegister")]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            var newUser = new IdentityUser
            {
                UserName = registerVm.Username,
                Email = registerVm.Email,
            };

            var identityRes = await userManager.CreateAsync(newUser, registerVm.Password);

            if (identityRes.Succeeded)
            {
                // assign role to user
                var roleIdentityRes = await userManager.AddToRoleAsync(newUser, "User");

                if (roleIdentityRes.Succeeded)
                {
                    TempData["msg"] = "Succes Created User";
                }

                TempData["msgErr"] = "Failed Created User";
            }

            TempData["msgErr"] = "Failed Created User";
            return RedirectToAction("Register"); ;
        }


        [HttpGet, Route("/auth/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("DoLogin")]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            var signInRes = await signInManager.PasswordSignInAsync(loginVm.Username, loginVm.Password, false, false);
            
            if (signInRes.Succeeded && signInRes != null)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["msgErr"] = "Username / Password Incorrect!";
            return RedirectToAction("Login");
        }
    }
}
