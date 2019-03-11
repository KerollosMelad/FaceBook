using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaceBook_InitialVersion.Models;
using Microsoft.AspNetCore.Identity;
using FaceBook_InitialVersion.Data;

namespace FaceBook_InitialVersion.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Person> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<Role> roleManager;



        public HomeController(UserManager<Person> _userManager, ApplicationDbContext _dbContext, RoleManager<Role> _roleManager)
        {
            userManager = _userManager;
            dbContext = _dbContext;
            roleManager = _roleManager;
        }
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");
            else if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index", "Posts");

            }
            else
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
