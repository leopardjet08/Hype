namespace FinalProject.DAL.Migrations
{
    using FinalProject.Models;
    using FinalProject.Models.DataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalProject.DAL.JobPostingCFEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\Migrations";
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception e)
            {
                throw new Exception(
                     "Seed Failed - errors follow:\n" +
                     e.InnerException.InnerException.Message.ToString(), e
                 ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(FinalProject.DAL.JobPostingCFEntities context)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //var qualification = new List<Qualification>()
            //{
            //    new Qualification{ QualificationSet="Qualification1"},
            //    new Qualification{ QualificationSet="Qualification2"},
            //    new Qualification{ QualificationSet="Qualification3"},
            //    new Qualification{ QualificationSet="Qualification4"},
            //    new Qualification{ QualificationSet="Qualification5"},
            //    new Qualification{ QualificationSet="Qualification6"},
            //    new Qualification{ QualificationSet="Qualification7"},
            //};
            //qualification.ForEach(a => context.Qualifications.AddOrUpdate(n => n.QualificationSet, a));
            //SaveChanges(context);

            //var requirements = new List<Requirement>()
            //{
            //    new Requirement{ RequirementName="Diploma"},
            //    new Requirement{ RequirementName="Certificate"},
            //    new Requirement{ RequirementName="Book"},
            //    new Requirement{ RequirementName="Pencil"},
            //    new Requirement{ RequirementName="Shoes"},
            //    new Requirement{ RequirementName="T-shirt"},
            //    new Requirement{ RequirementName="Hat"},
            //};
            //requirements.ForEach(a => context.Requirements.AddOrUpdate(n => n.RequirementName, a));
            //SaveChanges(context);


            //// Seeding Jobs individually to manually put Qualification and Requirement Seed Data
            //var job1 = new Job { Qualifications = new List<Qualification>(), Requirements = new List<Requirement>(), JobTitle = "Math Teacher", JobSummary = "Math teacher should be good as me. She/he knows how to add,subtract,multiplication and divide. He also know how to do statistic." };
            //var job2 = new Job { Qualifications = new List<Qualification>(), Requirements = new List<Requirement>(), JobTitle = "Librarian", JobSummary = "She knows how to read and write. Good reading skills and very knowlegable as me." };
            //var job3 = new Job { Qualifications = new List<Qualification>(), Requirements = new List<Requirement>(), JobTitle = "Janitor", JobSummary = "Very good at handling mops and brushes. He should always be around who can adpat very fast in the environment" };

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Requirement and Qualification Seed Data is in the Job Seed data itself
            var jobs = new List<Job>
            {
                new Job { JobCode="MATR", SkillQualification=false, Qualifications = new List<Qualification> { new Qualification { QualificationSet = "Qualification Set 1"}, new Qualification { QualificationSet = "Qualification Set 2"} },
                    Skills = new List<Skill> { new Skill { SkillName = "Skill 1"}, new Skill { SkillName = "Skill 2"} },
                    Requirements = new List<Requirement> { new Requirement { RequirementName = "Critical Thinking" }, new Requirement { RequirementName = "Adaptability" } },
                    JobTitle ="Math Teacher",JobSummary="Math teacher should be good as me. She/he knows how to add,subtract,multiplication and divide. He also know how to do statistic."},


                new Job { JobCode="LIR",SkillQualification=true, Qualifications = new List<Qualification> { new Qualification { QualificationSet = "Qualification Set 3"}, new Qualification { QualificationSet = "Qualification Set 4"} },
                    Skills = new List<Skill> { new Skill { SkillName = "Skill 22"}, new Skill { SkillName = "Skill 5"}, new Skill { SkillName = "Skill 9"} },
                    Requirements = new List<Requirement> {new Requirement { RequirementName = "Charismatic" }, new Requirement { RequirementName = "Integrity" } },
                    JobTitle ="Librarian",JobSummary="She knows how to read and write. Good reading skills and very knowlegable as me."},

                new Job { JobCode="JAT",SkillQualification=true, Qualifications = new List<Qualification> { new Qualification { QualificationSet = "Qualification Set 5"} },
                    Skills = new List<Skill> { new Skill { SkillName = "Skill 13"}, new Skill { SkillName = "Skill 24"} },
                    Requirements = new List<Requirement> {new Requirement { RequirementName = "Punctuality" } },
                    JobTitle ="Janitor",JobSummary="Very good at handling mops and brushes. He should always be around who can adpat very fast in the environment"}

            };
            jobs.ForEach(a => context.Jobs.AddOrUpdate(n => n.JobTitle, a));
            SaveChanges(context);

            var provinces = new List<Province>
            {
                new Province { ProvinceName="Ontario"},
                new Province { ProvinceName="Quebec"},
                new Province { ProvinceName="Nova Scotia"},
                new Province { ProvinceName="New Brunswick"},
                new Province { ProvinceName="Manitoba"},
                new Province { ProvinceName="British Columbia"},
                new Province { ProvinceName="Saskatchewan"},
                new Province { ProvinceName="Alberta"},
                new Province { ProvinceName="Newfoundland and Labrador"}
            };
            provinces.ForEach(a => context.Provinces.AddOrUpdate(n => n.ProvinceName, a));
            SaveChanges(context);

            var city = new List<City>
            {
                new City { ID=1, CityName="Welland"},
                new City { ID=2,CityName="St. Catharines"},
                new City { ID=3,CityName="Niagara Falls"},
                new City { ID=4,CityName="Thorold"},
                new City { ID=5,CityName="Grimsby"},
                new City { ID=6,CityName="Fort Erie"},
                new City { ID=7,CityName="Pelham"},
                new City { ID=8,CityName="Lincoln & W. Lincoln"},
                new City { ID=9,CityName="Wainfleet"},
                new City { ID=10,CityName="Port Colborne"},
                new City { ID=11,CityName="Niagara-on-the-Lake"}
            };
            city.ForEach(a => context.Cities.AddOrUpdate(n => n.CityName, a));
            SaveChanges(context);

            var applicationStatuses = new List<ApplicationStatus>
            {
                new ApplicationStatus { Status = "Pending" },
                new ApplicationStatus { Status = "Declined" },
                new ApplicationStatus { Status = "Accepted" }
            };
            applicationStatuses.ForEach(a => context.ApplicationStatus.AddOrUpdate(n => n.Status, a));
            SaveChanges(context);

            var schoolLevels = new List<SchoolLevel>
            {
                new SchoolLevel { LevelName="Elementary Schools"},
                new SchoolLevel { LevelName="Secondary Schools"}
            };
            schoolLevels.ForEach(a => context.SchoolLevels.AddOrUpdate(n => n.LevelName, a));
            SaveChanges(context);


            var schoolFamilies = new List<SchoolFamily>
            {
                new SchoolFamily { FamilyName="Notre Dame"},
                new SchoolFamily { FamilyName="Holy Cross"},
                new SchoolFamily { FamilyName="Saint Paul"},
                new SchoolFamily { FamilyName="Saint Michael"},
                new SchoolFamily { FamilyName="AAAAAAAAAA"},
                new SchoolFamily { FamilyName="Denis Morris"},
                new SchoolFamily { FamilyName="Saint Francis"},
                new SchoolFamily { FamilyName="Blessed Trinity"},
                new SchoolFamily { FamilyName="Lakeshore Catholic"}

            };
            schoolFamilies.ForEach(a => context.SchoolFamilies.AddOrUpdate(n => n.FamilyName, a));
            SaveChanges(context);


            var school = new List<School>
            {
                new School { SchoolName="Alexander Kuska",SchoolLevelID=1,CityID=1,SchoolFamilyID=1},
                new School { SchoolName="Assumption",SchoolLevelID=1,CityID=2,SchoolFamilyID=2},//2
                new School { SchoolName="Canadian Martyrs",SchoolLevelID=1,CityID=2,SchoolFamilyID=2},
                new School { SchoolName="Cardinal Newman",SchoolLevelID=1,CityID=3,SchoolFamilyID=3},
                new School { SchoolName="Father Hennepin",SchoolLevelID=1,CityID=3,SchoolFamilyID=4},//3
                new School { SchoolName="Holy Name",SchoolLevelID=1,CityID=1,SchoolFamilyID=1},
                new School { SchoolName="Loretto Catholic",SchoolLevelID=1,CityID=3,SchoolFamilyID=4},
                new School { SchoolName="Mary Ward",SchoolLevelID=1,CityID=3,SchoolFamilyID=3},
                new School { SchoolName="Monsignor Clancy",SchoolLevelID=1,CityID=4,SchoolFamilyID=6},//4
                new School { SchoolName="Mother Teresa",SchoolLevelID=1,CityID=2,SchoolFamilyID=7},
                new School { SchoolName="Notre Dame",SchoolLevelID=1,CityID=3,SchoolFamilyID=3},
                new School { SchoolName="Our Lady of Fatima",SchoolLevelID=1,CityID=5,SchoolFamilyID=8},
                new School { SchoolName="Our Lady of Fatima",SchoolLevelID=1,CityID=2,SchoolFamilyID=2},
                new School { SchoolName="Our Lady of Mount Carmel",SchoolLevelID=1,CityID=3,SchoolFamilyID=4},
                new School { SchoolName="Our Lady of Victory",SchoolLevelID=1,CityID=6,SchoolFamilyID=9},
                new School { SchoolName="Sacred Heart",SchoolLevelID=1,CityID=3,SchoolFamilyID=4},
                new School { SchoolName="St. Alexander",SchoolLevelID=1,CityID=7,SchoolFamilyID=1},
                new School { SchoolName="St. Alfred",SchoolLevelID=1,CityID=2,SchoolFamilyID=2},
                new School { SchoolName="St. Andrew",SchoolLevelID=1,CityID=1,SchoolFamilyID=1},
                new School { SchoolName="St. Ann",SchoolLevelID=1,CityID=7,SchoolFamilyID=1},
                new School { SchoolName="St. Ann",SchoolLevelID=1,CityID=2,SchoolFamilyID=7},
                new School { SchoolName="St. Anthony",SchoolLevelID=1,CityID=2,SchoolFamilyID=6},
                new School { SchoolName="St. Augustine",SchoolLevelID=1,CityID=1,SchoolFamilyID=1},
                new School { SchoolName="St. Charles",SchoolLevelID=1,CityID=4,SchoolFamilyID=6},
                new School { SchoolName="St. Christopher",SchoolLevelID=1,CityID=2,SchoolFamilyID=6},
                new School { SchoolName="St. Denis",SchoolLevelID=1,CityID=2,SchoolFamilyID=7},
                new School { SchoolName="St. Edward",SchoolLevelID=1,CityID=8,SchoolFamilyID=8},
                new School { SchoolName="St. Elizabeth",SchoolLevelID=1,CityID=9,SchoolFamilyID=9},
                new School { SchoolName="St. Gabriel Lalemant",SchoolLevelID=1,CityID=3,SchoolFamilyID=3},
                new School { SchoolName="St. George",SchoolLevelID=1,CityID=6,SchoolFamilyID=9},
                new School { SchoolName="St. James",SchoolLevelID=1,CityID=2,SchoolFamilyID=7},
                new School { SchoolName="St. John",SchoolLevelID=1,CityID=8,SchoolFamilyID=8},
                new School { SchoolName="St. John Bosco",SchoolLevelID=1,CityID=10,SchoolFamilyID=9},
                new School { SchoolName="St. Joseph",SchoolLevelID=1,CityID=6,SchoolFamilyID=9},
                new School { SchoolName="St. Joseph",SchoolLevelID=1,CityID=5,SchoolFamilyID=8},
                new School { SchoolName="St. Kevin",SchoolLevelID=1,CityID=1,SchoolFamilyID=1},
                new School { SchoolName="St. Mark",SchoolLevelID=1,CityID=8,SchoolFamilyID=8},
                new School { SchoolName="St. Martin",SchoolLevelID=1,CityID=8,SchoolFamilyID=8},
                new School { SchoolName="St. Mary",SchoolLevelID=1,CityID=3,SchoolFamilyID=3},
                new School { SchoolName="St. Mary",SchoolLevelID=1,CityID=1,SchoolFamilyID=1},
                new School { SchoolName="St. Michael",SchoolLevelID=1,CityID=11,SchoolFamilyID=2},
                new School { SchoolName="St. Nicholas",SchoolLevelID=1,CityID=2,SchoolFamilyID=6},
                new School { SchoolName="St. Patrick",SchoolLevelID=1,CityID=3,SchoolFamilyID=3},
                new School { SchoolName="St. Patrick",SchoolLevelID=1,CityID=10,SchoolFamilyID=9},
                new School { SchoolName="St. Peter",SchoolLevelID=1,CityID=2,SchoolFamilyID=6},
                new School { SchoolName="St. Philomena",SchoolLevelID=1,CityID=6,SchoolFamilyID=9},
                new School { SchoolName="St. Theresa",SchoolLevelID=1,CityID=2,SchoolFamilyID=6},
                new School { SchoolName="St. Therese",SchoolLevelID=1,CityID=10,SchoolFamilyID=9},
                new School { SchoolName="St. Vincent de Paul",SchoolLevelID=1,CityID=3,SchoolFamilyID=3}

            };
            school.ForEach(a => context.Schools.AddOrUpdate(n => n.SchoolName, a));
            SaveChanges(context);

            var applicants = new List<Applicant>
            {
                new Applicant { FName = "Alex", MName = "Ark",  LName = "Axibeg", PhoneNumber=9053707878, EMail="aaxibeg@outlook.com", Address="45 road street", ProvinceID=1,CityID=2},
                new Applicant { FName = "Boris", MName = "Boyle",  LName = "Burnsworth", PhoneNumber=9055669878, EMail="bburnsworth@outlook.com", Address="55 street", ProvinceID=3,CityID=1},
                new Applicant { FName = "Cathy", LName = "Carlisle",PhoneNumber=9053557878,  EMail="ccarlisle@outlook.com" , Address="11 street", ProvinceID=2,CityID=4},
                new Applicant { FName = "Derrick", MName = "Dee",  LName = "DaVinci", PhoneNumber=9053741184, EMail="ddavincis@outlook.com", Address="88 street", ProvinceID=4,CityID=2}
            };
            applicants.ForEach(a => context.Applicants.AddOrUpdate(n => n.EMail, a));
            SaveChanges(context);

            var postings = new List<Posting>
            {
                new Posting {

                    NumberOpen =2,ClosingDate=DateTime.Parse("2019-02-20"),StartDate=DateTime.Parse("2020-01-02"),JobEndDate=DateTime.Parse("2021-01-02"),
                PostingDescription="Math teachers work with students of all ages in classrooms around the United States. Typically, they teach children and teens, but some math teachers may also teach adults, or continue their careers at community colleges or universities. Their goal is to help pupils develop critical-thinking abilities by gaining an understanding of mathematic concepts.", SchoolID=2,
                JobID=2, Fte = 0.4, JobCode="MATR",SkillQualification=false},

                new Posting { 

                    NumberOpen =1,ClosingDate=DateTime.Parse("2019-05-10"),StartDate=DateTime.Parse("2020-04-02"),JobEndDate=DateTime.Parse("2022-05-02"),
                PostingDescription="Librarians evaluate books and other informational resources for consideration as additions to collections.They organize resources so that patrons can easily find the material that they desire. Librarians assess the research needs of individual visitors and identify the necessary resources.Librarians arrange speakers, entertainers and workshops to educate and entertain patrons.They publicize services to their constituency and endeavor to expand the use of library resources.", SchoolID=5,
                JobID=1, Fte=0.7, JobCode="LIR",SkillQualification=true},

                new Posting { 

                    NumberOpen =3,ClosingDate=DateTime.Parse("2019-09-09"),StartDate=DateTime.Parse("2020-08-06"),JobEndDate=DateTime.Parse("2023-01-02"),
                PostingDescription="Keep buildings in clean and orderly condition. Perform heavy cleaning duties, such as cleaning floors, shampooing rugs, washing walls and glass, and removing rubbish. Duties may include tending furnace and boiler, performing routine maintenance activities, notifying management of need for repairs, and cleaning snow or debris from sidewalk.", SchoolID=9,
                JobID=3, Fte=1.3, JobCode="JAT",SkillQualification=true}

            };
            postings.ForEach(a => context.Postings.AddOrUpdate(n => n.ClosingDate, a));
            SaveChanges(context);



            var applications = new List<Application>
            {
                new Application {  ApplicantID=(context.Applicants.Where(p=>p.EMail=="aaxibeg@outlook.com").SingleOrDefault().ID),
                    PostingID =(context.Postings.Where(p=>p.NumberOpen==2).SingleOrDefault().ID), ApplicationStatusID=1 },
                new Application {  ApplicantID=(context.Applicants.Where(p=>p.EMail=="bburnsworth@outlook.com").SingleOrDefault().ID),
                    PostingID =(context.Postings.Where(p=>p.NumberOpen==3).SingleOrDefault().ID), ApplicationStatusID=2 },
                new Application {  ApplicantID=(context.Applicants.Where(p=>p.EMail=="ccarlisle@outlook.com").SingleOrDefault().ID),
                    PostingID =(context.Postings.Where(p=>p.NumberOpen==1).SingleOrDefault().ID), ApplicationStatusID=3 }
            };
            applications.ForEach(a => context.Applications.AddOrUpdate(n => n.ApplicantID, a));
            SaveChanges(context);

            var bestCandiate = new List<BestCandidate>
            {
                new BestCandidate { ApplicationID=3},

            };
            bestCandiate.ForEach(a => context.BestCandidates.AddOrUpdate(n => n.ApplicationID, a));
            SaveChanges(context);

            var savePosting = new List<SavedPosting>()
            {
                new SavedPosting{ ApplicantID=1, PostingID=1},
                new SavedPosting{ ApplicantID=2, PostingID=2}
            };
            savePosting.ForEach(a => context.SavedPostings.AddOrUpdate(n => n.ApplicantID, a));
            SaveChanges(context);



        }

    }
}

