using EcommerceWeb.CustomerSite.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.CustomerSite.Controllers
{
    public class AuthController : Controller
    {
        private IData _data;

        public AuthController(IData data)
        {
            _data = data;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel input, string returnUrl)
        {
            returnUrl = HttpContext.Request.Query["returnUrl"];
            if (!ModelState.IsValid)
            {
                return View();
            }
            var token = await _data.LoginAsync(input);
            if (token == null)
            {
                ModelState.AddModelError("Error", "Your account is not valid. Try again!");
                return View();
            }
            else
            {
                var claims = new List<Claim>();
                foreach (var role in token.UserInfo.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                List<Claim> userclaims = new List<Claim>
                {
                    new Claim("token", "Bearer " + token.TokenString),
                    new Claim("expiration", token.Expiration.ToString()),
                    new Claim("userid", token.UserInfo.Id.ToString()),
                    new Claim("username", token.UserInfo.UserName)
                }; 
                claims.AddRange(userclaims);

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrinciple);

                TempData["Message"] = "Login success";
                return RedirectToAction("Index", "Products");
            }
        }
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }

        public async Task<ActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register([Bind("Username, Email, Password")] RegisterModel registerModel, string confirmPassword)
        {
            if (registerModel.Password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Confirm password field must match with password field");
            }
            if (ModelState.IsValid)
            {
                await _data.RegisterAsync(registerModel);
            }
            else
            {
                return View();
            }
            TempData["Message"] = "Register success";
            return RedirectToAction("Login", "Auth");
        }
    }
}
