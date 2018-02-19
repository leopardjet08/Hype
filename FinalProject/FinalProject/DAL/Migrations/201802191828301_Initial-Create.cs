namespace FinalProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false, maxLength: 50),
                        MName = c.String(maxLength: 50),
                        LName = c.String(nullable: false, maxLength: 50),
                        eMail = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 80),
                        ProvinceID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .Index(t => t.eMail, unique: true, name: "IX_Unique_Applicant_email")
                .Index(t => t.ProvinceID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostingID = c.Int(nullable: false),
                        ApplicantID = c.Int(nullable: false),
                        ApplicationStatusID = c.Int(nullable: false),
                        School_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("dbo.Posting", t => t.PostingID)
                .ForeignKey("dbo.School", t => t.School_ID)
                .Index(t => t.PostingID, unique: true, name: "IX_Unique_Posting")
                .Index(t => t.ApplicantID, unique: true, name: "IX_Unique_Application")
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.School_ID);
            
            CreateTable(
                "dbo.ApplicationStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Posting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOpen = c.Int(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        PostingDescription = c.String(maxLength: 2000),
                        SchoolID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                        SavedPosting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.JobID)
                .ForeignKey("dbo.School", t => t.SchoolID)
                .ForeignKey("dbo.SavedPosting", t => t.SavedPosting_ID)
                .Index(t => t.SchoolID)
                .Index(t => t.JobID)
                .Index(t => t.SavedPosting_ID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 50),
                        JobSummary = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Qualification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QualificationSet = c.String(nullable: false, maxLength: 255),
                        Job_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.Job_ID)
                .Index(t => t.QualificationSet, unique: true, name: "IX_Unique_Skill")
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.Requirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequirementName = c.String(),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.JobID)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(nullable: false, maxLength: 80),
                        SchoolLevelID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SchoolFamilyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.SchoolFamily", t => t.SchoolFamilyID)
                .ForeignKey("dbo.SchoolLevel", t => t.SchoolLevelID)
                .Index(t => t.SchoolLevelID)
                .Index(t => t.CityID)
                .Index(t => t.SchoolFamilyID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.CityName, unique: true, name: "IX_Unique_City");
            
            CreateTable(
                "dbo.SchoolFamily",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.FamilyName, unique: true, name: "IX_Unique_FamilyName");
            
            CreateTable(
                "dbo.SchoolLevel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.LevelName, unique: true, name: "IX_Unique_LevelName");
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ProvinceName, unique: true, name: "IX_Unique_Province");
            
            CreateTable(
                "dbo.SavedPosting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID)
                .Index(t => t.ApplicantID);
            
            CreateTable(
                "dbo.BestCandidate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.UserPhoto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BestCandidate", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.Posting", "SavedPosting_ID", "dbo.SavedPosting");
            DropForeignKey("dbo.SavedPosting", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.Applicant", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.School", "SchoolLevelID", "dbo.SchoolLevel");
            DropForeignKey("dbo.School", "SchoolFamilyID", "dbo.SchoolFamily");
            DropForeignKey("dbo.Posting", "SchoolID", "dbo.School");
            DropForeignKey("dbo.School", "CityID", "dbo.City");
            DropForeignKey("dbo.Applicant", "CityID", "dbo.City");
            DropForeignKey("dbo.Application", "School_ID", "dbo.School");
            DropForeignKey("dbo.Requirement", "JobID", "dbo.Job");
            DropForeignKey("dbo.Qualification", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.Posting", "JobID", "dbo.Job");
            DropForeignKey("dbo.Application", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.Application", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.Application", "ApplicantID", "dbo.Applicant");
            DropIndex("dbo.BestCandidate", new[] { "ApplicationID" });
            DropIndex("dbo.SavedPosting", new[] { "ApplicantID" });
            DropIndex("dbo.Province", "IX_Unique_Province");
            DropIndex("dbo.SchoolLevel", "IX_Unique_LevelName");
            DropIndex("dbo.SchoolFamily", "IX_Unique_FamilyName");
            DropIndex("dbo.City", "IX_Unique_City");
            DropIndex("dbo.School", new[] { "SchoolFamilyID" });
            DropIndex("dbo.School", new[] { "CityID" });
            DropIndex("dbo.School", new[] { "SchoolLevelID" });
            DropIndex("dbo.Requirement", new[] { "JobID" });
            DropIndex("dbo.Qualification", new[] { "Job_ID" });
            DropIndex("dbo.Qualification", "IX_Unique_Skill");
            DropIndex("dbo.Posting", new[] { "SavedPosting_ID" });
            DropIndex("dbo.Posting", new[] { "JobID" });
            DropIndex("dbo.Posting", new[] { "SchoolID" });
            DropIndex("dbo.Application", new[] { "School_ID" });
            DropIndex("dbo.Application", new[] { "ApplicationStatusID" });
            DropIndex("dbo.Application", "IX_Unique_Application");
            DropIndex("dbo.Application", "IX_Unique_Posting");
            DropIndex("dbo.Applicant", new[] { "CityID" });
            DropIndex("dbo.Applicant", new[] { "ProvinceID" });
            DropIndex("dbo.Applicant", "IX_Unique_Applicant_email");
            DropTable("dbo.UserPhoto");
            DropTable("dbo.BestCandidate");
            DropTable("dbo.SavedPosting");
            DropTable("dbo.Province");
            DropTable("dbo.SchoolLevel");
            DropTable("dbo.SchoolFamily");
            DropTable("dbo.City");
            DropTable("dbo.School");
            DropTable("dbo.Requirement");
            DropTable("dbo.Qualification");
            DropTable("dbo.Job");
            DropTable("dbo.Posting");
            DropTable("dbo.ApplicationStatus");
            DropTable("dbo.Application");
            DropTable("dbo.Applicant");
        }
    }
}
