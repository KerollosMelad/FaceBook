using System;
using FaceBook_InitialVersion.Models;
using FaceBook_InitialVersion.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace FaceBook_InitialVersion.Controllers
{

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<Person> userManager;

        public AdminController(ApplicationDbContext _dbContext, RoleManager<Role> _roleManager, UserManager<Person> _userManager)
        {
            dbContext = _dbContext;
            roleManager = _roleManager;
            userManager = _userManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //  var p = new Person() { FirstName="shehab"};

            //var ClaimsIdentity2 = (ClaimsIdentity)this.User.Identity;
            //var claim = ClaimsIdentity2.FindFirst(ClaimTypes.NameIdentifier);

            var xx = userManager.GetUsersInRoleAsync("Member");

            return View(await xx);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string Searchtext)
        {

            var xx = await userManager.GetUsersInRoleAsync("Member");
            var SearchResult = (from p in xx
                                where p.FirstName.Contains(Searchtext) || p.LastName.Contains(Searchtext) || p.Email.Contains(Searchtext)
                                select p).ToList();


            return PartialView("Search", SearchResult);

        }
        [HttpPost]
        public async Task<IActionResult> searching(string Searchtext)
        {
            if (Searchtext == null)
            {
                return PartialView("Search", await userManager.GetUsersInRoleAsync("Member"));

            }

            var xx = await userManager.GetUsersInRoleAsync("Member");
            var SearchResult = (from p in xx
                                where p.FirstName.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase)
                                || p.LastName.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase) ||
                                p.Email.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase)
                                select p).ToList();

            HttpContext.Session.SetString("SearchText", Searchtext);
            return PartialView("Search", SearchResult);

        }
        public async Task<IActionResult> Activate(string id)
        {


            var person = dbContext.Users.FirstOrDefault(per => per.Id == id);
            person.State = Enums.UserStatus.Active;

            dbContext.SaveChanges();

            string Searchtext = HttpContext.Session.GetString("SearchText");
            //if (Searchtext == null)
            //{
            //    return PartialView("Search", await userManager.GetUsersInRoleAsync("Member"));

            //}
            var xx = await userManager.GetUsersInRoleAsync("Member");

            var SearchResult = (from p in xx
                                where p.FirstName.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase)
                                || p.LastName.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase) ||
                                p.Email.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase)
                                select p).ToList();
            return PartialView("Search", SearchResult);
        }
        public async Task<IActionResult> Block(string id)
        {
          
            var person = dbContext.Users.FirstOrDefault(per => per.Id == id);
            person.State = Enums.UserStatus.Blocked;
            dbContext.SaveChanges();

            string Searchtext = HttpContext.Session.GetString("SearchText");
            //if (Searchtext == null)
            //{
            //    return PartialView("Search", await userManager.GetUsersInRoleAsync("Member"));

            //}
            var xx = await userManager.GetUsersInRoleAsync("Member");

            var SearchResult = (from p in xx
                                where p.FirstName.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase)
                                || p.LastName.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase) ||
                                p.Email.Contains(Searchtext, StringComparison.CurrentCultureIgnoreCase)
                                select p).ToList();
            return PartialView("Search", SearchResult);

        }
    }
}