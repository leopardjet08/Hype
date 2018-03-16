namespace FinalProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hjkhkj : DbMigration
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
                        CreatedBy = c.String(maxLength: 256),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedOn = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
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
                        SubmissionDate = c.DateTime(nullable: false),
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
                        fte = c.Double(nullable: false),
                        SchoolID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedOn = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.JobID)
                .ForeignKey("dbo.School", t => t.SchoolID)
                .Index(t => t.SchoolID)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 50),
                        JobSummary = c.String(maxLength: 2000),
                        SkillQualification = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Qualification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QualificationSet = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.QualificationSet, unique: true, name: "IX_Unique_Qualification");
            
            CreateTable(
                "dbo.Requirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequirementName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.RequirementName, unique: true, name: "IX_Unique_Requirement");
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.SkillName, unique: true, name: "IX_Unique_Skill");
            
            CreateTable(
                "dbo.SavedPosting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        PostingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID)
                .ForeignKey("dbo.Posting", t => t.PostingID)
                .Index(t => t.ApplicantID)
                .Index(t => t.PostingID);
            
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
            
            CreateTable(
                "dbo.QualificationJob",
                c => new
                    {
                        Qualification_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Qualification_ID, t.Job_ID })
                .ForeignKey("dbo.Qualification", t => t.Qualification_ID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Qualification_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.RequirementJob",
                c => new
                    {
                        Requirement_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Requirement_ID, t.Job_ID })
                .ForeignKey("dbo.Requirement", t => t.Requirement_ID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Requirement_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.SkillJob",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Job_ID })
                .ForeignKey("dbo.Skill", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Job_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BestCandidate", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.Applicant", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.School", "SchoolLevelID", "dbo.SchoolLevel");
            DropForeignKey("dbo.School", "SchoolFamilyID", "dbo.SchoolFamily");
            DropForeignKey("dbo.Posting", "SchoolID", "dbo.School");
            DropForeignKey("dbo.School", "CityID", "dbo.City");
            DropForeignKey("dbo.Applicant", "CityID", "dbo.City");
            DropForeignKey("dbo.Application", "School_ID", "dbo.School");
            DropForeignKey("dbo.SavedPosting", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.SavedPosting", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.SkillJob", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.SkillJob", "Skill_ID", "dbo.Skill");
            DropForeignKey("dbo.RequirementJob", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.RequirementJob", "Requirement_ID", "dbo.Requirement");
            DropForeignKey("dbo.QualificationJob", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.QualificationJob", "Qualification_ID", "dbo.Qualification");
            DropForeignKey("dbo.Posting", "JobID", "dbo.Job");
            DropForeignKey("dbo.Application", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.Application", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.Application", "ApplicantID", "dbo.Applicant");
            DropIndex("dbo.SkillJob", new[] { "Job_ID" });
            DropIndex("dbo.SkillJob", new[] { "Skill_ID" });
            DropIndex("dbo.RequirementJob", new[] { "Job_ID" });
            DropIndex("dbo.RequirementJob", new[] { "Requirement_ID" });
            DropIndex("dbo.QualificationJob", new[] { "Job_ID" });
            DropIndex("dbo.QualificationJob", new[] { "Qualification_ID" });
            DropIndex("dbo.BestCandidate", new[] { "ApplicationID" });
            DropIndex("dbo.Province", "IX_Unique_Province");
            DropIndex("dbo.SchoolLevel", "IX_Unique_LevelName");
            DropIndex("dbo.SchoolFamily", "IX_Unique_FamilyName");
            DropIndex("dbo.City", "IX_Unique_City");
            DropIndex("dbo.School", new[] { "SchoolFamilyID" });
            DropIndex("dbo.School", new[] { "CityID" });
            DropIndex("dbo.School", new[] { "SchoolLevelID" });
            DropIndex("dbo.SavedPosting", new[] { "PostingID" });
            DropIndex("dbo.SavedPosting", new[] { "ApplicantID" });
            DropIndex("dbo.Skill", "IX_Unique_Skill");
            DropIndex("dbo.Requirement", "IX_Unique_Requirement");
            DropIndex("dbo.Qualification", "IX_Unique_Qualification");
            DropIndex("dbo.Posting", new[] { "JobID" });
            DropIndex("dbo.Posting", new[] { "SchoolID" });
            DropIndex("dbo.Application", new[] { "School_ID" });
            DropIndex("dbo.Application", new[] { "ApplicationStatusID" });
            DropIndex("dbo.Application", "IX_Unique_Application");
            DropIndex("dbo.Application", "IX_Unique_Posting");
            DropIndex("dbo.Applicant", new[] { "CityID" });
            DropIndex("dbo.Applicant", new[] { "ProvinceID" });
            DropIndex("dbo.Applicant", "IX_Unique_Applicant_email");
            DropTable("dbo.SkillJob");
            DropTable("dbo.RequirementJob");
            DropTable("dbo.QualificationJob");
            DropTable("dbo.UserPhoto");
            DropTable("dbo.BestCandidate");
            DropTable("dbo.Province");
            DropTable("dbo.SchoolLevel");
            DropTable("dbo.SchoolFamily");
            DropTable("dbo.City");
            DropTable("dbo.School");
            DropTable("dbo.SavedPosting");
            DropTable("dbo.Skill");
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
