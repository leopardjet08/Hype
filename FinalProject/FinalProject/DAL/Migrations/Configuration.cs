namespace FinalProject.DAL.Migrations
{
    using FinalProject.Models;
    using FinalProject.Models.DataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Drawing;
    using System.IO;
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
                new Job { JobCode="MATR", SkillQualification=false, Qualifications = new List<Qualification> { new Qualification { QualificationSet = "Qualification Set 1"}, new Qualification { QualificationSet = "Qualification Set 2"}
                , new Qualification { QualificationSet = "Qualification Set 3"}, new Qualification { QualificationSet = "Qualification Set 4"}, new Qualification { QualificationSet = "Qualification Set 5"}, new Qualification { QualificationSet = "Qualification Set 6"}},
                    Requirements = new List<Requirement> { new Requirement { RequirementName = "Requirement 1" }, new Requirement { RequirementName = "Requirement 2" } },
                    JobTitle ="Math Teacher",JobSummary="Math teacher should be good as me. She/he knows how to add,subtract,multiplication and divide. He also know how to do statistic."},


                new Job { JobCode="LIR",SkillQualification=true, 
                    Skills = new List<Skill> { new Skill { SkillName = "Skill 1"}, new Skill { SkillName = "Skill 2"}, new Skill { SkillName = "Skill 3"} },
                    Requirements = new List<Requirement> {new Requirement { RequirementName = "Requirement 3" }, new Requirement { RequirementName = "Requirement 4" } },
                    JobTitle ="Librarian",JobSummary="She knows how to read and write. Good reading skills and very knowlegable as me."},

                new Job { JobCode="JAT",SkillQualification=true, 
                    Skills = new List<Skill> { new Skill { SkillName = "Skill 4"}, new Skill { SkillName = "Skill 5"} },
                    Requirements = new List<Requirement> {new Requirement { RequirementName = "Requirement 5" } },
                    JobTitle ="Janitor",JobSummary="Very good at handling mops and brushes. He should always be around who can adpat very fast in the environment"},

                new Job { JobCode="DRV",SkillQualification=true,
                    Skills = new List<Skill> { new Skill { SkillName = "Skill 6" }, new Skill { SkillName = "Skill 7" } },
                    Requirements = new List<Requirement> {new Requirement { RequirementName = "Requirement 6" },new Requirement { RequirementName = "Requirement 7" } },
                    JobTitle ="Driver",JobSummary="Very good at Driving in the Road. He can drive 200 km/h. "},

                new Job { JobCode="PrPAL",SkillQualification=false, Qualifications = new List<Qualification> { new Qualification { QualificationSet = "Qualification 7"} },
                    Requirements = new List<Requirement> {new Requirement { RequirementName = "Requirement 8" } },
                    JobTitle ="Principal",JobSummary="Very good handling corrupted teacher. Good at counting money and maintaining the school"}

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

            var postingStatus = new List<PostingStatus>
            {
                new PostingStatus { Status = "Active" },
                new PostingStatus { Status = "InActive" }
            };
            postingStatus.ForEach(a => context.PostingStatus.AddOrUpdate(n => n.Status, a));
            SaveChanges(context);

            var postingType = new List<PostingType>
            {
                new PostingType { Type = "Part-Time" },
                new PostingType { Type = "Full-Time" }
            };
            postingType.ForEach(a => context.PostingTypes.AddOrUpdate(n => n.Type, a));
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
                new Applicant { FName = "Derrick", MName = "Dee",  LName = "DaVinci", PhoneNumber=9053741184, EMail="ddavincis@outlook.com", Address="88 street", ProvinceID=4,CityID=2},
                new Applicant { FName = "Admin", MName = "Admin",  LName = "Admin", PhoneNumber=9053749999, EMail="admin@outlook.com", Address="Admin street", ProvinceID=1,CityID=6},
                new Applicant { FName = "Jet", MName = "Jet",  LName = "Jet", PhoneNumber=9053748888, EMail="jet@outlook.com", Address="jet street", ProvinceID=1,CityID=5}
            };
            applicants.ForEach(a => context.Applicants.AddOrUpdate(n => n.EMail, a));
            SaveChanges(context);




            //For applicant image to save in the database. 
            //uncomment this out and choose your path what image u want to save in database.

            //var applicantImage = new List<ApplicantImage>
            //{
            //    //The ImageToArray method will return the byte[] of an image
            //     new ApplicantImage {ApplicantImageID=1, ImageContent =  ImageToArray(@"C:\Users\raldj\Desktop\1.jpg"),ImageMimeType="1.jpg" },
            //     new ApplicantImage {ApplicantImageID=2, ImageContent =  ImageToArray(@"C:\Users\raldj\Desktop\3.jpg"),ImageMimeType="3.jpg" },
            //     new ApplicantImage {ApplicantImageID=3, ImageContent =  ImageToArray(@"C:\Users\raldj\Desktop\2.jpg"),ImageMimeType="2.jpg" },
            //     new ApplicantImage {ApplicantImageID=4, ImageContent =  ImageToArray(@"C:\Users\raldj\Desktop\4.jpg"),ImageMimeType="4.jpg" },
            //     new ApplicantImage {ApplicantImageID=5, ImageContent =  ImageToArray(@"C:\Users\raldj\Desktop\5.jpg"),ImageMimeType="5.jpg" },
            //     new ApplicantImage {ApplicantImageID=6, ImageContent =  ImageToArray(@"C:\Users\raldj\Desktop\6.jpg"),ImageMimeType="6.jpg" }

            //};
            //applicantImage.ForEach(s => context.ApplicantImages.AddOrUpdate(p => p.ApplicantImageID, s));






            var postings = new List<Posting>
            {
                new Posting {

                    NumberOpen =2,ClosingDate=DateTime.Parse("2019-02-20"),StartDate=DateTime.Parse("2020-01-02"),JobEndDate=DateTime.Parse("2021-01-02"),
                PostingDescription="Math teachers work with students of all ages in classrooms around the United States. Typically, they teach children and teens, but some math teachers may also teach adults, or continue their careers at community colleges or universities. Their goal is to help pupils develop critical-thinking abilities by gaining an understanding of mathematic concepts.", SchoolID=2,
                JobID=2, Fte = 0.4, JobCode="MATR",SkillQualification=false,PostingStatusID=2},

                new Posting { 

                    NumberOpen =1,ClosingDate=DateTime.Parse("2019-05-10"),StartDate=DateTime.Parse("2020-04-02"),JobEndDate=DateTime.Parse("2022-05-02"),
                PostingDescription="Librarians evaluate books and other informational resources for consideration as additions to collections.They organize resources so that patrons can easily find the material that they desire. Librarians assess the research needs of individual visitors and identify the necessary resources.Librarians arrange speakers, entertainers and workshops to educate and entertain patrons.They publicize services to their constituency and endeavor to expand the use of library resources.", SchoolID=5,
                JobID=1, Fte=0.7, JobCode="LIR",SkillQualification=true,PostingStatusID=1},

                new Posting { 

                    NumberOpen =3,ClosingDate=DateTime.Parse("2019-09-09"),StartDate=DateTime.Parse("2020-08-06"),JobEndDate=DateTime.Parse("2023-01-02"),
                PostingDescription="Keep buildings in clean and orderly condition. Perform heavy cleaning duties, such as cleaning floors, shampooing rugs, washing walls and glass, and removing rubbish. Duties may include tending furnace and boiler, performing routine maintenance activities, notifying management of need for repairs, and cleaning snow or debris from sidewalk.", SchoolID=9,
                JobID=3, Fte=1.3, JobCode="JAT",SkillQualification=true,PostingStatusID=1},

                new Posting {

                    NumberOpen =5,ClosingDate=DateTime.Parse("2018-03-09"),StartDate=DateTime.Parse("2018-08-06"),JobEndDate=DateTime.Parse("2019-01-02"),
                PostingDescription="Keep buildings in clean and orderly condition. Perform heavy cleaning duties, such as cleaning floors, shampooing rugs, washing walls and glass, and removing rubbish. Duties may include tending furnace and boiler, performing routine maintenance activities, notifying management of need for repairs, and cleaning snow or debris from sidewalk.", SchoolID=19,
                JobID=3, Fte=1.2, JobCode="JAT",SkillQualification=true,PostingStatusID=2}

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
            applications.ForEach(a => context.applications.AddOrUpdate(n => n.ApplicantID, a));
            SaveChanges(context);

            var applicationComment = new List<ApplicationComment>
            {
                new ApplicationComment { Comments = "Part-Time",ApplicationID=2 },
            };
            applicationComment.ForEach(a => context.ApplicationComments.AddOrUpdate(n => n.Comments, a));
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
                new SavedPosting{ ApplicantID=2, PostingID=1},
                new SavedPosting{ ApplicantID=3, PostingID=1},
                new SavedPosting{ ApplicantID=4, PostingID=1},
                new SavedPosting{ ApplicantID=5, PostingID=1},
                new SavedPosting{ ApplicantID=6, PostingID=1},
                new SavedPosting{ ApplicantID=1, PostingID=2},
                new SavedPosting{ ApplicantID=2, PostingID=2},
                new SavedPosting{ ApplicantID=3, PostingID=2},
                new SavedPosting{ ApplicantID=4, PostingID=2},
                new SavedPosting{ ApplicantID=5, PostingID=2},
                new SavedPosting{ ApplicantID=6, PostingID=2},
                new SavedPosting{ ApplicantID=1, PostingID=3},
                new SavedPosting{ ApplicantID=2, PostingID=3},
                new SavedPosting{ ApplicantID=3, PostingID=3},
                new SavedPosting{ ApplicantID=4, PostingID=3},
                new SavedPosting{ ApplicantID=5, PostingID=3},
                new SavedPosting{ ApplicantID=6, PostingID=3}
            };
            savePosting.ForEach(a => context.SavedPostings.AddOrUpdate(n => n.ApplicantID, a));
            SaveChanges(context);

            var appliedPosting = new List<Appliedposting>()
            {
                new Appliedposting{ ApplicantID=1, PostingID=1},
                new Appliedposting{ ApplicantID=2, PostingID=1},
                new Appliedposting{ ApplicantID=3, PostingID=1},
                new Appliedposting{ ApplicantID=4, PostingID=1},
                new Appliedposting{ ApplicantID=5, PostingID=1},
                new Appliedposting{ ApplicantID=6, PostingID=1},
                new Appliedposting{ ApplicantID=1, PostingID=2},
                new Appliedposting{ ApplicantID=2, PostingID=2},
                new Appliedposting{ ApplicantID=3, PostingID=2},
                new Appliedposting{ ApplicantID=4, PostingID=2},
                new Appliedposting{ ApplicantID=5, PostingID=2},
                new Appliedposting{ ApplicantID=6, PostingID=2},
                new Appliedposting{ ApplicantID=1, PostingID=3},
                new Appliedposting{ ApplicantID=2, PostingID=3},
                new Appliedposting{ ApplicantID=3, PostingID=3},
                new Appliedposting{ ApplicantID=4, PostingID=3},
                new Appliedposting{ ApplicantID=5, PostingID=3},
                new Appliedposting{ ApplicantID=6, PostingID=3}
            };
            appliedPosting.ForEach(a => context.Appliedpostings.AddOrUpdate(n => n.ApplicantID, a));
            SaveChanges(context);

            var expiredPosting = new List<ExpiredPosting>()
            {
                new ExpiredPosting{ ApplicantID=1, PostingID=4,ExpiredOn=DateTime.Parse("2018-03-09")},
                new ExpiredPosting{ ApplicantID=2, PostingID=4,ExpiredOn=DateTime.Parse("2018-03-09")},
                new ExpiredPosting{ ApplicantID=3, PostingID=4,ExpiredOn=DateTime.Parse("2018-03-09")},
                new ExpiredPosting{ ApplicantID=4, PostingID=4,ExpiredOn=DateTime.Parse("2018-03-09")},
                new ExpiredPosting{ ApplicantID=5, PostingID=4,ExpiredOn=DateTime.Parse("2018-03-09")},
                new ExpiredPosting{ ApplicantID=6, PostingID=4,ExpiredOn=DateTime.Parse("2018-03-09")}
            };
            expiredPosting.ForEach(a => context.ExpiredPostings.AddOrUpdate(n => n.ApplicantID, a));
            SaveChanges(context);

            var archiveposting = new List<Archiveposting>
            {
                new Archiveposting {

                    NumberOpen =2,ClosingDate=DateTime.Parse("2017-01-20"),StartDate=DateTime.Parse("2018-01-02"),JobEndDate=DateTime.Parse("2019-01-02"),
                PostingDescription="Math teachers work with students of all ages in classrooms around the United States. Typically, they teach children and teens, but some math teachers may also teach adults, or continue their careers at community colleges or universities. Their goal is to help pupils develop critical-thinking abilities by gaining an understanding of mathematic concepts.", SchoolID=5,
                JobID=2, fte = 0.4, JobCode="MATR",SkillQualification=false,ArchiveDate=DateTime.Parse("2017-02-20")},

                new Archiveposting {

                    NumberOpen =1,ClosingDate=DateTime.Parse("2014-05-10"),StartDate=DateTime.Parse("2014-07-02"),JobEndDate=DateTime.Parse("2015-05-02"),
                PostingDescription="Librarians evaluate books and other informational resources for consideration as additions to collections.They organize resources so that patrons can easily find the material that they desire. Librarians assess the research needs of individual visitors and identify the necessary resources.Librarians arrange speakers, entertainers and workshops to educate and entertain patrons.They publicize services to their constituency and endeavor to expand the use of library resources.", SchoolID=5,
                JobID=1, fte=0.7, JobCode="LIR",SkillQualification=true,ArchiveDate=DateTime.Parse("2014-02-20")},

                new Archiveposting {

                    NumberOpen =3,ClosingDate=DateTime.Parse("2011-01-09"),StartDate=DateTime.Parse("2011-03-06"),JobEndDate=DateTime.Parse("2012-01-02"),
                PostingDescription="Keep buildings in clean and orderly condition. Perform heavy cleaning duties, such as cleaning floors, shampooing rugs, washing walls and glass, and removing rubbish. Duties may include tending furnace and boiler, performing routine maintenance activities, notifying management of need for repairs, and cleaning snow or debris from sidewalk.", SchoolID=9,
                JobID=3, fte=1.3, JobCode="JAT",SkillQualification=true,ArchiveDate=DateTime.Parse("2011-02-20")}

            };
            archiveposting.ForEach(a => context.Archivepostings.AddOrUpdate(n => n.ClosingDate, a));
            SaveChanges(context);

        }

        //to get the image content and convert it to memory stream to save in the database
        public byte[] ImageToArray(String s)
        {
            Image img = Image.FromFile(s);
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            return arr;
        }
        //get the file type fro the databse
        private string GetMimeType(String filename)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(filename).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

    }
}

