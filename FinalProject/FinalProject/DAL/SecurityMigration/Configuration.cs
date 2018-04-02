namespace FinalProject.DAL.SecurityMigration
{
    using FinalProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalProject.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\SecurityMigration";
        }

        protected override void Seed(ApplicationDbContext context)
        {
             //Create a Role Manager
            var roleManager = new RoleManager<IdentityRole>(new
                                          RoleStore<IdentityRole>(context));
            //Create Role Admin if it does not exist
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }
            //Create Role Staff if it does not exist
            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Staff"));
            }

            //Create a User Manager
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //-----------------------------------------
            //Now the Admin user named admin1 with password password
            var adminuser = new ApplicationUser
            {
                UserName = "admin@outlook.com",
                Email = "admin@outlook.com"
            };

            //Assign admin user to role
            if (!context.Users.Any(u => u.UserName == "admin@outlook.com"))
            {
                manager.Create(adminuser, "qwe");
                manager.AddToRole(adminuser.Id, "Admin");
                manager.AddToRole(adminuser.Id, "Staff");
            }

            //-----------------------------------------------------
            //Now the Staff user named staff1 with password password
            var staffuser = new ApplicationUser
            {
                UserName = "staff@outlook.com",
                Email = "staff@outlook.com"
            };

            //Assign staff user to role
            if (!context.Users.Any(u => u.UserName == "staff1@outlook.com"))
            {
                manager.Create(staffuser, "qwe");
                manager.AddToRole(staffuser.Id, "Staff");
            }
            //Create a few generic users
            for (int i = 1; i <= 4; i++)
            {
                var user = new ApplicationUser
                {
                    UserName = string.Format("user{0}@outlook.com", i.ToString()),
                    Email = string.Format("user{0}@outlook.com", i.ToString())
                };
                if (!context.Users.Any(u => u.UserName == user.UserName))
                    manager.Create(user, "qwe");
            }
        }
    }
}
