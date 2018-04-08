using FinalProject.Models;
using FinalProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        public DbSet<Application> applications { get; set; }
        public DbSet<BestCandidate> BestCandidates { get; set; }
        public DbSet<SavedPosting> SavedPostings { get; set; }
        public DbSet<aFile> Files { get; set; }
        public DbSet<PostingStatus> PostingStatus { get; set; }
        public DbSet<ArchiveApplication> ArchiveApplications { get; set; }
        public DbSet<Archiveposting> Archivepostings { get; set; }
        public DbSet<Appliedposting> Appliedpostings { get; set; }
        public DbSet<ExpiredPosting> ExpiredPostings { get; set; }
        public DbSet<ApplicationComment> ApplicationComments { get; set; }
        public DbSet<PostingType> PostingTypes { get; set; }
        public DbSet<ApplicantImage> ApplicantImages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.HasDefaultSchema("Job");


            
            modelBuilder.Entity<Applicant>()
                .HasOptional(w => w.ApplicantImage)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);


            //Added for cascade delete for Applicant
            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Files)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.SavedPostings)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Appliedpostings)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.ExpiredPostings)
                .WithRequired(p => p.Applicant)
                .WillCascadeOnDelete(true);

            // for posting cascade delete
            modelBuilder.Entity<Posting>()
               .HasMany(a => a.Applications)
               .WithRequired(p => p.Posting)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Posting>()
              .HasMany(a => a.SavedPostings)
              .WithRequired(p => p.Postings)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<Posting>()
              .HasMany(a => a.AppliedPostings)
              .WithRequired(p => p.Postings)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<Posting>()
              .HasMany(a => a.ExpiredPostings)
              .WithRequired(p => p.Postings)
              .WillCascadeOnDelete(false);



            //application cascade


        }

        public override int SaveChanges()
        {
            //Get Audit Values if not supplied
            string auditUser = "Anonymous";
            try //Need to try becuase HttpContext might not exist
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    auditUser = HttpContext.Current.User.Identity.Name;
            }
            catch (Exception)
            { }

            DateTime auditDate = DateTime.UtcNow;
            foreach (DbEntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = auditDate;
                    entry.Entity.CreatedBy = auditUser;
                    entry.Entity.UpdatedOn = auditDate;
                    entry.Entity.UpdatedBy = auditUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedOn = auditDate;
                    entry.Entity.UpdatedBy = auditUser;
                }
            }
            return base.SaveChanges();
        }

       
    }
        
    }
