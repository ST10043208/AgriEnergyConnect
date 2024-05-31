using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;
using AgriEnergyConnect.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);                    
                    if (result.Succeeded)
                    {
                        UserClass.loggedIn = true;
                        UserClass.role = user.Role;
                        return RedirectToLocal(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        return View("Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // Check if the email is already registered
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(model);
                }

                user = new ApplicationUser { UserName = model.Name, Email = model.Email, 
                    PhoneNumber = model.PhoneNumber, Role = model.Role, EmailConfirmed = true, 
                    PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled =false, AccessFailedCount = 0};
                _context.ApplicationUsers.Add(user);

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Role)
                    {
                        var employee = new Employee
                        {
                            Id = user.Id,                                           
                            ApplicationUser = user
                        };
                        _context.Employees.Add(employee);
                    }
                    else
                    {
                        var farmer = new Farmer
                        {
                            Id = user.Id,
                            Name = model.Name,
                            Email = model.Email,
                            ApplicationUser = user
                        };
                        _context.Farmers.Add(farmer);
                    }
                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    UserClass.loggedIn = true;
                    UserClass.role = user.Role;
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            await _signInManager.SignOutAsync();
            UserClass.loggedIn = false;
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }

}
