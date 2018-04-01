namespace FinalProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JetInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Job.Applicant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false, maxLength: 50),
                        MName = c.String(maxLength: 50),
                        LName = c.String(nullable: false, maxLength: 50),
                        EMail = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.Long(nullable: false),
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
                .ForeignKey("Job.City", t => t.CityID)
                .ForeignKey("Job.Province", t => t.ProvinceID)
                .Index(t => t.EMail, unique: true, name: "IX_Unique_Applicant_email")
                .Index(t => t.ProvinceID)
                .Index(t => t.CityID);
            
            CreateTable(
                "Job.ApplicantImage",
                c => new
                    {
                        ApplicantImageID = c.Int(nullable: false),
                        ImageContent = c.Binary(),
                        ImageMimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ApplicantImageID)
                .ForeignKey("Job.Applicant", t => t.ApplicantImageID, cascadeDelete: true)
                .Index(t => t.ApplicantImageID);
            
            CreateTable(
                "Job.Application",
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
                .ForeignKey("Job.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("Job.Posting", t => t.PostingID)
                .ForeignKey("Job.School", t => t.School_ID)
                .ForeignKey("Job.Applicant", t => t.ApplicantID, cascadeDelete: true)
                .Index(t => t.PostingID, unique: true, name: "IX_Unique_Posting")
                .Index(t => t.ApplicantID, unique: true, name: "IX_Unique_Application")
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.School_ID);
            
            CreateTable(
                "Job.ApplicationStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Job.Posting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOpen = c.Int(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        JobEndDate = c.DateTime(nullable: false),
                        PostingDescription = c.String(maxLength: 2000),
                        Fte = c.Double(nullable: false),
                        SchoolID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                        JobCode = c.String(nullable: false, maxLength: 20),
                        SkillQualification = c.Boolean(nullable: false),
                        PostingStatusID = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedOn = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Job.Job", t => t.JobID)
                .ForeignKey("Job.PostingStatus", t => t.PostingStatusID)
                .ForeignKey("Job.School", t => t.SchoolID)
                .Index(t => t.SchoolID)
                .Index(t => t.JobID)
                .Index(t => t.PostingStatusID);
            
            CreateTable(
                "Job.Job",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobCode = c.String(nullable: false, maxLength: 20),
                        JobTitle = c.String(nullable: false, maxLength: 50),
                        JobSummary = c.String(maxLength: 2000),
                        SkillQualification = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.JobCode, unique: true, name: "IX_Unique_JobCode");
            
            CreateTable(
                "Job.Qualification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QualificationSet = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.QualificationSet, unique: true, name: "IX_Unique_qual");
            
            CreateTable(
                "Job.Requirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequirementName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.RequirementName, unique: true, name: "IX_Unique_Req");
            
            CreateTable(
                "Job.Skill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.SkillName, unique: true, name: "IX_Unique_skill");
            
            CreateTable(
                "Job.PostingStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Job.SavedPosting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        PostingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Job.Posting", t => t.PostingID)
                .ForeignKey("Job.Applicant", t => t.ApplicantID, cascadeDelete: true)
                .Index(t => t.ApplicantID)
                .Index(t => t.PostingID);
            
            CreateTable(
                "Job.School",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(nullable: false, maxLength: 80),
                        SchoolLevelID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SchoolFamilyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Job.City", t => t.CityID)
                .ForeignKey("Job.SchoolFamily", t => t.SchoolFamilyID)
                .ForeignKey("Job.SchoolLevel", t => t.SchoolLevelID)
                .Index(t => t.SchoolLevelID)
                .Index(t => t.CityID)
                .Index(t => t.SchoolFamilyID);
            
            CreateTable(
                "Job.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.CityName, unique: true, name: "IX_Unique_City");
            
            CreateTable(
                "Job.SchoolFamily",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.FamilyName, unique: true, name: "IX_Unique_FamilyName");
            
            CreateTable(
                "Job.SchoolLevel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.LevelName, unique: true, name: "IX_Unique_LevelName");
            
            CreateTable(
                "Job.aFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fileName = c.String(maxLength: 256),
                        Description = c.String(maxLength: 256),
                        ApplicantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Job.Applicant", t => t.ApplicantID, cascadeDelete: true)
                .Index(t => t.ApplicantID);
            
            CreateTable(
                "Job.FileContent",
                c => new
                    {
                        FileContentID = c.Int(nullable: false),
                        Content = c.Binary(),
                        MimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FileContentID)
                .ForeignKey("Job.aFile", t => t.FileContentID, cascadeDelete: true)
                .Index(t => t.FileContentID);
            
            CreateTable(
                "Job.Province",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ProvinceName, unique: true, name: "IX_Unique_Province");
            
            CreateTable(
                "Job.BestCandidate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Job.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "Job.QualificationJob",
                c => new
                    {
                        Qualification_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Qualification_ID, t.Job_ID })
                .ForeignKey("Job.Qualification", t => t.Qualification_ID, cascadeDelete: true)
                .ForeignKey("Job.Job", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Qualification_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "Job.QualificationPosting",
                c => new
                    {
                        Qualification_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Qualification_ID, t.Posting_ID })
                .ForeignKey("Job.Qualification", t => t.Qualification_ID, cascadeDelete: true)
                .ForeignKey("Job.Posting", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Qualification_ID)
                .Index(t => t.Posting_ID);
            
            CreateTable(
                "Job.RequirementJob",
                c => new
                    {
                        Requirement_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Requirement_ID, t.Job_ID })
                .ForeignKey("Job.Requirement", t => t.Requirement_ID, cascadeDelete: true)
                .ForeignKey("Job.Job", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Requirement_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "Job.RequirementPosting",
                c => new
                    {
                        Requirement_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Requirement_ID, t.Posting_ID })
                .ForeignKey("Job.Requirement", t => t.Requirement_ID, cascadeDelete: true)
                .ForeignKey("Job.Posting", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Requirement_ID)
                .Index(t => t.Posting_ID);
            
            CreateTable(
                "Job.SkillJob",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Job_ID })
                .ForeignKey("Job.Skill", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("Job.Job", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "Job.SkillPosting",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Posting_ID })
                .ForeignKey("Job.Skill", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("Job.Posting", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Posting_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Job.BestCandidate", "ApplicationID", "Job.Application");
            DropForeignKey("Job.SavedPosting", "ApplicantID", "Job.Applicant");
            DropForeignKey("Job.Applicant", "ProvinceID", "Job.Province");
            DropForeignKey("Job.aFile", "ApplicantID", "Job.Applicant");
            DropForeignKey("Job.FileContent", "FileContentID", "Job.aFile");
            DropForeignKey("Job.Application", "ApplicantID", "Job.Applicant");
            DropForeignKey("Job.School", "SchoolLevelID", "Job.SchoolLevel");
            DropForeignKey("Job.School", "SchoolFamilyID", "Job.SchoolFamily");
            DropForeignKey("Job.Posting", "SchoolID", "Job.School");
            DropForeignKey("Job.School", "CityID", "Job.City");
            DropForeignKey("Job.Applicant", "CityID", "Job.City");
            DropForeignKey("Job.Application", "School_ID", "Job.School");
            DropForeignKey("Job.SavedPosting", "PostingID", "Job.Posting");
            DropForeignKey("Job.Posting", "PostingStatusID", "Job.PostingStatus");
            DropForeignKey("Job.SkillPosting", "Posting_ID", "Job.Posting");
            DropForeignKey("Job.SkillPosting", "Skill_ID", "Job.Skill");
            DropForeignKey("Job.SkillJob", "Job_ID", "Job.Job");
            DropForeignKey("Job.SkillJob", "Skill_ID", "Job.Skill");
            DropForeignKey("Job.RequirementPosting", "Posting_ID", "Job.Posting");
            DropForeignKey("Job.RequirementPosting", "Requirement_ID", "Job.Requirement");
            DropForeignKey("Job.RequirementJob", "Job_ID", "Job.Job");
            DropForeignKey("Job.RequirementJob", "Requirement_ID", "Job.Requirement");
            DropForeignKey("Job.QualificationPosting", "Posting_ID", "Job.Posting");
            DropForeignKey("Job.QualificationPosting", "Qualification_ID", "Job.Qualification");
            DropForeignKey("Job.QualificationJob", "Job_ID", "Job.Job");
            DropForeignKey("Job.QualificationJob", "Qualification_ID", "Job.Qualification");
            DropForeignKey("Job.Posting", "JobID", "Job.Job");
            DropForeignKey("Job.Application", "PostingID", "Job.Posting");
            DropForeignKey("Job.Application", "ApplicationStatusID", "Job.ApplicationStatus");
            DropForeignKey("Job.ApplicantImage", "ApplicantImageID", "Job.Applicant");
            DropIndex("Job.SkillPosting", new[] { "Posting_ID" });
            DropIndex("Job.SkillPosting", new[] { "Skill_ID" });
            DropIndex("Job.SkillJob", new[] { "Job_ID" });
            DropIndex("Job.SkillJob", new[] { "Skill_ID" });
            DropIndex("Job.RequirementPosting", new[] { "Posting_ID" });
            DropIndex("Job.RequirementPosting", new[] { "Requirement_ID" });
            DropIndex("Job.RequirementJob", new[] { "Job_ID" });
            DropIndex("Job.RequirementJob", new[] { "Requirement_ID" });
            DropIndex("Job.QualificationPosting", new[] { "Posting_ID" });
            DropIndex("Job.QualificationPosting", new[] { "Qualification_ID" });
            DropIndex("Job.QualificationJob", new[] { "Job_ID" });
            DropIndex("Job.QualificationJob", new[] { "Qualification_ID" });
            DropIndex("Job.BestCandidate", new[] { "ApplicationID" });
            DropIndex("Job.Province", "IX_Unique_Province");
            DropIndex("Job.FileContent", new[] { "FileContentID" });
            DropIndex("Job.aFile", new[] { "ApplicantID" });
            DropIndex("Job.SchoolLevel", "IX_Unique_LevelName");
            DropIndex("Job.SchoolFamily", "IX_Unique_FamilyName");
            DropIndex("Job.City", "IX_Unique_City");
            DropIndex("Job.School", new[] { "SchoolFamilyID" });
            DropIndex("Job.School", new[] { "CityID" });
            DropIndex("Job.School", new[] { "SchoolLevelID" });
            DropIndex("Job.SavedPosting", new[] { "PostingID" });
            DropIndex("Job.SavedPosting", new[] { "ApplicantID" });
            DropIndex("Job.Skill", "IX_Unique_skill");
            DropIndex("Job.Requirement", "IX_Unique_Req");
            DropIndex("Job.Qualification", "IX_Unique_qual");
            DropIndex("Job.Job", "IX_Unique_JobCode");
            DropIndex("Job.Posting", new[] { "PostingStatusID" });
            DropIndex("Job.Posting", new[] { "JobID" });
            DropIndex("Job.Posting", new[] { "SchoolID" });
            DropIndex("Job.Application", new[] { "School_ID" });
            DropIndex("Job.Application", new[] { "ApplicationStatusID" });
            DropIndex("Job.Application", "IX_Unique_Application");
            DropIndex("Job.Application", "IX_Unique_Posting");
            DropIndex("Job.ApplicantImage", new[] { "ApplicantImageID" });
            DropIndex("Job.Applicant", new[] { "CityID" });
            DropIndex("Job.Applicant", new[] { "ProvinceID" });
            DropIndex("Job.Applicant", "IX_Unique_Applicant_email");
            DropTable("Job.SkillPosting");
            DropTable("Job.SkillJob");
            DropTable("Job.RequirementPosting");
            DropTable("Job.RequirementJob");
            DropTable("Job.QualificationPosting");
            DropTable("Job.QualificationJob");
            DropTable("Job.BestCandidate");
            DropTable("Job.Province");
            DropTable("Job.FileContent");
            DropTable("Job.aFile");
            DropTable("Job.SchoolLevel");
            DropTable("Job.SchoolFamily");
            DropTable("Job.City");
            DropTable("Job.School");
            DropTable("Job.SavedPosting");
            DropTable("Job.PostingStatus");
            DropTable("Job.Skill");
            DropTable("Job.Requirement");
            DropTable("Job.Qualification");
            DropTable("Job.Job");
            DropTable("Job.Posting");
            DropTable("Job.ApplicationStatus");
            DropTable("Job.Application");
            DropTable("Job.ApplicantImage");
            DropTable("Job.Applicant");
        }
    }
}
