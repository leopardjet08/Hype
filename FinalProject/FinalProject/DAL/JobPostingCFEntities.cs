using FinalProject.Models;
using FinalProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FinalProject.DAL
{
    public class JobPostingCFEntities : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public DbSet<SchoolLevel> SchoolLevels { get; set; }
        public DbSet<SchoolFamily> SchoolFamilies { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Posting> Postings { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<BestCandidate> BestCandidates { get; set; }
        public DbSet<SavedPosting> SavedPostings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


        }

    }
    

}