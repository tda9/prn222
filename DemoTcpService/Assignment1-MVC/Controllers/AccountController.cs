using Assignment1_MVC.Models;
using Assignment1_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1_MVC.Controllers
{
    public class AccountController : Controller

    {
        private readonly FunewsManagementContext _context;

        public AccountController(FunewsManagementContext context)
        {
            this._context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Trim (and optionally lowercase) the input values
            string email = model.Email.Trim();
            string password = model.Password.Trim();

            var account = await _context.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountEmail == email &&
                                          a.AccountPassword == password);

            if (account == null)
            {
                ModelState.AddModelError("", "Email or password is incorrect.");
                return View(model);
            }

            Response.Cookies.Append("UserLoggedIn", "true");
            Response.Cookies.Append("Role", account.AccountRole.ToString());
            Response.Cookies.Append("AccountId", account.AccountId.ToString());
            
            return RedirectToAction("Index", "Home");
        }
 /*
        public IActionResult Register()
        {
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Password not match.");
                    return View(model);
                }
                SystemAccount user = new SystemAccount
                {
                    AccountName = model.Name,
                    AccountEmail = model.Email,
                    AccountPassword = model.Password,
                    AccountRole = 2
                };
                var result = _context.SystemAccounts.Add(user);
                _context.SaveChanges();
                //var result = await userManager.CreateAsync(users, model.Password);

                if (result != null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
            }
            return View(model);
        }*/

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("UserLoggedIn");
            Response.Cookies.Delete("Role");
            return RedirectToAction("Index", "Home");
        }
    }
}
