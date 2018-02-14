using FinalProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FinalProject.Models.Application> Applications { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.Applicant> Applicants { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.DataModel.ApplicationStatus> ApprovedApps { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.Posting> Postings { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.DataModel.City> Cities { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.Province> Provinces { get; set; }
    }
}