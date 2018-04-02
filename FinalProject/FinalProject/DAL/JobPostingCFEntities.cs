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
        public DbSet<aFile> Files { get; set; }
        public DbSet<PostingStatus> PostingStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.HasDefaultSchema("Job");


            //Added for cascade delete for applicant image profile picture
            modelBuilder.Entity<Applicant>()
                .HasOptional(w => w.ApplicantImage)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            //Added for cascade delte for all Files with Applicant
            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Files)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.SavedPostings)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Applicant>()
               .HasMany(a => a.Applications)
               .WithRequired(p => p.Applicant)
               .WillCascadeOnDelete(true);

            //Added for cascade delete for File Content with File
            modelBuilder.Entity<aFile>()
                .HasOptional(w => w.FileContent)
                .WithRequired(p => p.aFile)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<City>()
            //    .HasMany(w => w.Schools)
            //    .WithRequired(p => p.City)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<City>()
            // .HasMany(w => w.Applicants)
            // .WithRequired(p => p.City)
            // .WillCascadeOnDelete(false);

            //modelBuilder.Entity<School>()
            //.HasMany(w => w.Postings)
            //.WithRequired(p => p.School)
            //.WillCascadeOnDelete(true);

            //modelBuilder.Entity<School>()
            //.HasMany(w => w.Postings)
            //.WithRequired(p => p.School)
            //.WillCascadeOnDelete(true);

        }

        public System.Data.Entity.DbSet<FinalProject.Models.DataModel.ApplicantImage> ApplicantImages { get; set; }
    }
        
    }
