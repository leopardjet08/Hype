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

        protected override void Seed(FinalProject.DAL.ApplicationDbContext context)
        {
            //Create a Role Manager
            var roleManager = new RoleManager<IdentityRole>(new
                                          RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var roleresult = roleManager.Create(new IdentityRole("User"));
            }
            if (!context.Roles.Any(r => r.Name == "Security"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Security"));
            }


            if (!(context.Users.Any(u => u.Email == "aaxibeg@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user1 = new ApplicationUser
                {

                    UserName = "aaxibeg@outlook.com",
                    PhoneNumber = "9053707878",
                    Email = "aaxibeg@outlook.com"
                };

                userManager.Create(user1, "qwerty");
                userManager.AddToRole(user1.Id, "User");
            }

            if (!(context.Users.Any(u => u.Email == "bburnsworth@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user2 = new ApplicationUser
                {

                    UserName = "bburnsworth@outlook.com",
                    PhoneNumber = "9055669878",
                    Email = "bburnsworth@outlook.com"
                };

                userManager.Create(user2, "qwerty");
                userManager.AddToRole(user2.Id, "User");
            }

            if (!(context.Users.Any(u => u.Email == "ccarlisle@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user4 = new ApplicationUser
                {

                    UserName = "ccarlisle@outlook.com",
                    PhoneNumber = "9053557878",
                    Email = "ccarlisle@outlook.com"
                };

                userManager.Create(user4, "qwerty");
                userManager.AddToRole(user4.Id, "User");
            }

            if (!(context.Users.Any(u => u.Email == "ddavincis@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user3 = new ApplicationUser
                {

                    UserName = "ddavincis@outlook.com",
                    PhoneNumber = "9053741184",
                    Email = "ddavincis@outlook.com"
                };
                userManager.Create(user3, "qwerty");
                userManager.AddToRole(user3.Id, "User");
            }

            if (!(context.Users.Any(u => u.Email == "Admin@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin = new ApplicationUser
                {

                    UserName = "Admin@outlook.com",
                    PhoneNumber = "905343184",
                    Email = "Admin@outlook.com"
                };
                userManager.Create(admin, "qwerty");
                userManager.AddToRole(admin.Id, "Admin");
            }

            if (!(context.Users.Any(u => u.Email == "Admin@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin = new ApplicationUser
                {

                    UserName = "Admin@outlook.com",
                    PhoneNumber = "905343184",
                    Email = "Admin@outlook.com"
                };
                userManager.Create(admin, "qwerty");
                userManager.AddToRole(admin.Id, "Admin");
            }

            if (!(context.Users.Any(u => u.Email == "cassey@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin2 = new ApplicationUser
                {

                    UserName = "cassey@outlook.com",
                    PhoneNumber = "905343184",
                    Email = "cassey@outlook.com"
                };
                userManager.Create(admin2, "qwerty");
                userManager.AddToRole(admin2.Id, "Admin");
                userManager.AddToRole(admin2.Id, "User");
            }

            if (!(context.Users.Any(u => u.Email == "milan@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin3 = new ApplicationUser
                {

                    UserName = "milan@outlook.com",
                    PhoneNumber = "905343184",
                    Email = "milan@outlook.com"
                };
                userManager.Create(admin3, "qwerty");
                userManager.AddToRole(admin3.Id, "Admin");
                userManager.AddToRole(admin3.Id, "User");
            }

            if (!(context.Users.Any(u => u.Email == "jet@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin4 = new ApplicationUser
                {

                    UserName = "jet@outlook.com",
                    PhoneNumber = "905343184",
                    Email = "jet@outlook.com"
                };
                userManager.Create(admin4, "qwerty");
                userManager.AddToRole(admin4.Id, "Admin");
                userManager.AddToRole(admin4.Id, "User");
                userManager.AddToRole(admin4.Id, "Security");
            }

            if (!(context.Users.Any(u => u.Email == "lake@outlook.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin5 = new ApplicationUser
                {

                    UserName = "lake@outlook.com",
                    PhoneNumber = "905343184",
                    Email = "lake@outlook.com"
                };
                userManager.Create(admin5, "qwerty");
                userManager.AddToRole(admin5.Id, "Admin");
                userManager.AddToRole(admin5.Id, "User");
            }






        }
    }
}
