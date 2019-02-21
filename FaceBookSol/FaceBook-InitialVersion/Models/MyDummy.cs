﻿using FaceBook_InitialVersion.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceBook_InitialVersion.Models
{
    public class MyDummy
    {
        public static async Task Initialization(ApplicationDbContext context, UserManager<Person> userManager, RoleManager<Role> roleManager)
        {
            string role1 = "Admin";
            string role2 = "Member";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                roleManager.CreateAsync(new Role(role1, "This is the Admin", DateTime.Now)).Wait();
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                roleManager.CreateAsync(new Role(role2, "This is the Memeber", DateTime.Now)).Wait();
            }
            if (await userManager.FindByEmailAsync("z@z.com") == null)
            {
                var user = new Person()
                {
                    Email = "z@z.com",
                    BirthDay =  DateTime.Now,
                    FirstName = "John",
                    LastName = "Emad",
                    Gender = Enums.Gender.Male,
                    UserName = "z@z.com",
                    State=Enums.UserStatus.Active
                };
                await userManager.CreateAsync(user);
                await userManager.AddPasswordAsync(user, "Shehab@456");
                await userManager.AddToRoleAsync(user, role2);
            }
            if (await userManager.FindByEmailAsync("nb@b.com") == null)
            {
                var user = new Person()
                {
                    Email = "nb@b.com",
                    BirthDay = DateTime.Now,
                    FirstName = "Shehab",
                    LastName = "Mohsen",
                    Gender = Enums.Gender.Male,
                    UserName = "nb@b.com",
                    State = Enums.UserStatus.Active
                };
                await userManager.CreateAsync(user);
                await userManager.AddPasswordAsync(user, "Shehab@456");
                await userManager.AddToRoleAsync(user, role2);
            }
            await context.SaveChangesAsync();

        }

    }
}