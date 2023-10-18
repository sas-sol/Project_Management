using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Project_Management.Models.Account;
using System.Security.Claims;
using Project_Management.Data;
using Project_Management.Models.ViewModels;
using BCrypt.Net;


namespace Project_Management.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(ProjectContext context)
        {
            Context = context;
        }
        public ProjectContext Context { get; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = Context.Users.Where(e => e.Username == model.Username).SingleOrDefault();
                if (data != null)
                {
                    if (data.Role == "Admin")
                    {
                        //bool isValid = (data.Username == model.Username && data.Password == model.Password);
                        bool isValid = BCrypt.Net.BCrypt.Verify(model.Password, data.Password);
                        if (isValid)
                        {
                            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.NameIdentifier, data.Id.ToString())
                };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            // Set the "Remember Me" option based on the checkbox value
                            var authenticationProperties = new AuthenticationProperties();
                            if (model.IsRemember)
                            {
                                authenticationProperties.IsPersistent = true; // Cookie won't expire after the session ends
                                authenticationProperties.ExpiresUtc = DateTime.UtcNow.AddDays(30); // Set your desired expiration time
                            }

                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                            HttpContext.Session.SetString("Username", model.Username);
                            return RedirectToAction("GetPorject", "Project");
                        }
                        else
                        {
                            TempData["errorPassword"] = "Invalid Password";
                            return View(model);
                        }
                    }
                    else
                    {
                        //bool isValid = (data.Username == model.Username && data.Password == model.Password);
                        bool isValid = BCrypt.Net.BCrypt.Verify(model.Password, data.Password);
                        if (isValid)
                        {
                            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.NameIdentifier, data.Id.ToString())
                };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            // Set the "Remember Me" option based on the checkbox value
                            var authenticationProperties = new AuthenticationProperties();
                            if (model.IsRemember)
                            {
                                authenticationProperties.IsPersistent = true; // Cookie won't expire after the session ends
                                authenticationProperties.ExpiresUtc = DateTime.UtcNow.AddDays(30); // Set your desired expiration time
                            }

                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                            HttpContext.Session.SetString("Username", model.Username);
                            return RedirectToAction("GetPorject", "Project");
                        }
                        else
                        {
                            TempData["errorPassword"] = "Invalid Password";
                            return View(model);
                        }
                    }
                }
                else
                {
                    TempData["errorUsername"] = "Username not found!";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("Login", "Account");
        }


       

        public IActionResult FrontPage()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                var data = new Users()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = hashedPassword,
                    Role = "User",
                };
                Context.Users.Add(data);
                Context.SaveChanges();
                TempData["successMessage"] = "You are Registered Successfully";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorMessage"] = "Empty form can't be Submitted";
                return View(model);
            }
        }
    }
}
