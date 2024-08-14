using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Object.Data;
using Object.Models;
using Object.ViewModel.Account;

namespace Object.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context , UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
    }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Login Fail");
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVM);
        }
    }
}
