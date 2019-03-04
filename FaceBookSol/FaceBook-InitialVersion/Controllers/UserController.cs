using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FaceBook_InitialVersion.Data;
using FaceBook_InitialVersion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FaceBook_InitialVersion.Controllers
{
   

    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<Person> userManager;
        public UserController(ApplicationDbContext _dbContext, RoleManager<Role> _roleManager, UserManager<Person> _userManager)
        {
            dbContext = _dbContext;
            roleManager = _roleManager;
            userManager = _userManager;
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> IndexAsync()
        {
            var x = User;
            var ClaimsIdentity2 = (ClaimsIdentity)this.User.Identity;
            var claim = ClaimsIdentity2.FindFirst(ClaimTypes.NameIdentifier);

            var x2 = dbContext.Posts.Where(i => i.UserPostComments[0].userID == claim.Value);

            return View(x2.ToList());
        }
    }
}