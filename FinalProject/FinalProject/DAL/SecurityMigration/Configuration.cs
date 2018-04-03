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
            if (!(context.Users.Any(u => u.UserName == "Test@yahoo.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "Test@yahoo.com", PhoneNumber = "12345678911", Email = "Test@yahoo.com" };
                userManager.Create(userToInsert, "Password123!");
            }
        }
    }
}
