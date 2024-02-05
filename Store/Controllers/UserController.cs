using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Store.Models;
using Microsoft.AspNetCore.Authentication;

namespace Store.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext context;
        public UserController(UserDbContext context)
        {
            this.context = context;
        }
        public ViewResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Authorization(string name, string password)
        {
            if (name.IsNullOrEmpty() || password.IsNullOrEmpty())
                return View();

            User? user = context.Users.FirstOrDefault(p => p.Name == name && p.Password == password);

            if (user is null) return View();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }
        public ViewResult CreateNewUser()
        {
            return View("CreateNewUser");
        }
        public async Task<IActionResult> AddNewUser(string name, string password)
        {
            if (name.IsNullOrEmpty() || password.IsNullOrEmpty())
                return View();
            await context.Users.AddAsync(new User { Name = name, Password = password });
            await context.SaveChangesAsync();
            return await Authorization(name, password);
        }
    }
}
