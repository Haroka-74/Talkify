using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talkify.Models.Auth;
using Talkify.DTOs.AuthDTOs;
using Microsoft.EntityFrameworkCore;

namespace Talkify.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<TalkifyUser> userManager;
        private readonly SignInManager<TalkifyUser> signInManager;

        public AuthController(UserManager<TalkifyUser> userManager, SignInManager<TalkifyUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return View(registerDTO);

            if (await userManager.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == registerDTO.Email.ToLower()) is not null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(registerDTO);
            }

            var user = new TalkifyUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerDTO);
            }

            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            var result = await signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, loginDTO.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(loginDTO);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}