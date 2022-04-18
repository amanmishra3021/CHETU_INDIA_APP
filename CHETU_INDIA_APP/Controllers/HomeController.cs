using CHETU_INDIA_APP.DB_Context;
using CHETU_INDIA_APP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CHETU_INDIA_APP.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index(usermodel mod)
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            var user = ent.LoginInformationTables.Where(m => m.Email == mod.Email).FirstOrDefault();
            if (user == null)
            {
                TempData["invalid"] = "Email.is not found invalid user ";
            }
            else
            {
                if (user.Email == mod.Email && user.Password == mod.Password)
                {
                    var claims = new[] {new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Name,user.Name) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authproperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        authproperties);
                    HttpContext.Session.SetString("Role",user.Role);
                    HttpContext.Session.SetString("name", user.Name);
                    HttpContext.Session.GetString("name");
                    HttpContext.Session.GetString("Role");
                }
                else
                {
                    TempData["not valid"] = "wrong password";
                }
            }
            return View();
        }
        public IActionResult Add_emp()
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            List<empmodel> mod = new List<empmodel>();
            var res = ent.ChetuEmplyoyees.ToList();
            foreach (var item in res)
            {
                mod.Add(new empmodel
                {
                    Id=item.Id,
                    Name=item.Name,
                    Email=item.Email,
                    City=item.City,
                    Designation=item.Designation,
                    Mobile=item.Mobile,
                });

            }
            return View(mod);
        }
        [HttpGet]
        public IActionResult user_registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult user_registration(usermodel mod)
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            LoginInformationTable logtb = new LoginInformationTable();
            logtb.Id = mod.Id;
            logtb.Name = mod.Name;
            logtb.Email = mod.Email;
            logtb.Password = mod.Password;
            ent.LoginInformationTables.Add(logtb);
            ent.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult userlogin()
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            var res = ent.LoginInformationTables.ToList();
            return View(res);

        }
        [HttpGet]
        public IActionResult add_emp1()
        {
            return View();

        }
        [HttpPost]
        public IActionResult add_emp1(empmodel mod1)
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            ChetuEmplyoyee tb = new ChetuEmplyoyee();
            tb.Id = mod1.Id;
            tb.Name = mod1.Name;
            tb.Email = mod1.Email;
            tb.City = mod1.City;
            tb.Designation = mod1.Designation;
            tb.Mobile = mod1.Mobile;
            ent.ChetuEmplyoyees.Add(tb);
            ent.SaveChanges();
            return RedirectToAction("Add_emp");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Indexdashboard()
        {
            return View();
        }
        public IActionResult _leftnavbar()
        {
            return View();
        }
        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return View("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
