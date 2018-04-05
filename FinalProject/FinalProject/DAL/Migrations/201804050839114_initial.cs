namespace FinalProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantImage",
                c => new
                    {
                        ApplicantImageID = c.Int(nullable: false),
                        ImageContent = c.Binary(),
                        ImageMimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ApplicantImageID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantImageID, cascadeDelete: true)
                .Index(t => t.ApplicantImageID);
            
            CreateTable(
                "dbo.Applicant",
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
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .Index(t => t.EMail, unique: true, name: "IX_Unique_Applicant_email")
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
                        Archiveposting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("dbo.Posting", t => t.PostingID)
                .ForeignKey("dbo.School", t => t.School_ID)
                .ForeignKey("dbo.Archiveposting", t => t.Archiveposting_ID)
                .Index(t => t.PostingID)
                .Index(t => t.ApplicantID)
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.School_ID)
                .Index(t => t.Archiveposting_ID);
            
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
                        JobEndDate = c.DateTime(nullable: false),
                        PostingDescription = c.String(maxLength: 2000),
                        Fte = c.Double(nullable: false),
                        SchoolID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                        JobCode = c.String(nullable: false, maxLength: 20),
                        SkillQualification = c.Boolean(nullable: false),
                        PostingStatusID = c.Int(nullable: false),
                        PostingTypesID = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedOn = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.JobID)
                .ForeignKey("dbo.PostingStatus", t => t.PostingStatusID)
                .ForeignKey("dbo.PostingType", t => t.PostingTypesID)
                .ForeignKey("dbo.School", t => t.SchoolID)
                .Index(t => t.SchoolID)
                .Index(t => t.JobID)
                .Index(t => t.PostingStatusID)
                .Index(t => t.PostingTypesID);
            
            CreateTable(
                "dbo.Job",
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
                "dbo.Qualification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QualificationSet = c.String(nullable: false, maxLength: 255),
                        Archiveposting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Archiveposting", t => t.Archiveposting_ID)
                .Index(t => t.QualificationSet, unique: true, name: "IX_Unique_qual")
                .Index(t => t.Archiveposting_ID);
            
            CreateTable(
                "dbo.Requirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequirementName = c.String(nullable: false, maxLength: 50),
                        Archiveposting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Archiveposting", t => t.Archiveposting_ID)
                .Index(t => t.RequirementName, unique: true, name: "IX_Unique_Req")
                .Index(t => t.Archiveposting_ID);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false, maxLength: 255),
                        Archiveposting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Archiveposting", t => t.Archiveposting_ID)
                .Index(t => t.SkillName, unique: true, name: "IX_Unique_skill")
                .Index(t => t.Archiveposting_ID);
            
            CreateTable(
                "dbo.PostingStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostingType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SavedPosting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        PostingID = c.Int(nullable: false),
                        Archiveposting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posting", t => t.PostingID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID, cascadeDelete: true)
                .ForeignKey("dbo.Archiveposting", t => t.Archiveposting_ID)
                .Index(t => t.ApplicantID)
                .Index(t => t.PostingID)
                .Index(t => t.Archiveposting_ID);
            
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
                .PrimaryKey(t => t.ID);
            
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
                "dbo.aFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fileName = c.String(maxLength: 256),
                        Description = c.String(maxLength: 256),
                        ApplicantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID, cascadeDelete: true)
                .Index(t => t.ApplicantID);
            
            CreateTable(
                "dbo.FileContent",
                c => new
                    {
                        FileContentID = c.Int(nullable: false),
                        Content = c.Binary(),
                        MimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FileContentID)
                .ForeignKey("dbo.aFile", t => t.FileContentID)
                .Index(t => t.FileContentID);
            
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
                "dbo.ApplicationComment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.Appliedposting",
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
                "dbo.ArchiveApplication",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostingID = c.Int(nullable: false),
                        ApplicantID = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID)
                .ForeignKey("dbo.Posting", t => t.PostingID)
                .Index(t => t.PostingID)
                .Index(t => t.ApplicantID);
            
            CreateTable(
                "dbo.Archiveposting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOpen = c.Int(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        JobEndDate = c.DateTime(nullable: false),
                        PostingDescription = c.String(maxLength: 2000),
                        fte = c.Double(nullable: false),
                        SchoolID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                        JobCode = c.String(nullable: false, maxLength: 20),
                        SkillQualification = c.Boolean(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.JobID)
                .ForeignKey("dbo.School", t => t.SchoolID)
                .Index(t => t.SchoolID)
                .Index(t => t.JobID);
            
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
                "dbo.ExpiredPosting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        PostingID = c.Int(nullable: false),
                        ExpiredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicant", t => t.ApplicantID)
                .ForeignKey("dbo.Posting", t => t.PostingID)
                .Index(t => t.ApplicantID)
                .Index(t => t.PostingID);
            
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
                "dbo.QualificationPosting",
                c => new
                    {
                        Qualification_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Qualification_ID, t.Posting_ID })
                .ForeignKey("dbo.Qualification", t => t.Qualification_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posting", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Qualification_ID)
                .Index(t => t.Posting_ID);
            
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
                "dbo.RequirementPosting",
                c => new
                    {
                        Requirement_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Requirement_ID, t.Posting_ID })
                .ForeignKey("dbo.Requirement", t => t.Requirement_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posting", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Requirement_ID)
                .Index(t => t.Posting_ID);
            
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
            
            CreateTable(
                "dbo.SkillPosting",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Posting_ID })
                .ForeignKey("dbo.Skill", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posting", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Posting_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpiredPosting", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.ExpiredPosting", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.BestCandidate", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.Skill", "Archiveposting_ID", "dbo.Archiveposting");
            DropForeignKey("dbo.Archiveposting", "SchoolID", "dbo.School");
            DropForeignKey("dbo.SavedPosting", "Archiveposting_ID", "dbo.Archiveposting");
            DropForeignKey("dbo.Requirement", "Archiveposting_ID", "dbo.Archiveposting");
            DropForeignKey("dbo.Qualification", "Archiveposting_ID", "dbo.Archiveposting");
            DropForeignKey("dbo.Archiveposting", "JobID", "dbo.Job");
            DropForeignKey("dbo.Application", "Archiveposting_ID", "dbo.Archiveposting");
            DropForeignKey("dbo.ArchiveApplication", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.ArchiveApplication", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.Appliedposting", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.Appliedposting", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.ApplicationComment", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.SavedPosting", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.Applicant", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.aFile", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.FileContent", "FileContentID", "dbo.aFile");
            DropForeignKey("dbo.School", "SchoolLevelID", "dbo.SchoolLevel");
            DropForeignKey("dbo.School", "SchoolFamilyID", "dbo.SchoolFamily");
            DropForeignKey("dbo.Posting", "SchoolID", "dbo.School");
            DropForeignKey("dbo.School", "CityID", "dbo.City");
            DropForeignKey("dbo.Applicant", "CityID", "dbo.City");
            DropForeignKey("dbo.Application", "School_ID", "dbo.School");
            DropForeignKey("dbo.SavedPosting", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.Posting", "PostingTypesID", "dbo.PostingType");
            DropForeignKey("dbo.Posting", "PostingStatusID", "dbo.PostingStatus");
            DropForeignKey("dbo.SkillPosting", "Posting_ID", "dbo.Posting");
            DropForeignKey("dbo.SkillPosting", "Skill_ID", "dbo.Skill");
            DropForeignKey("dbo.SkillJob", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.SkillJob", "Skill_ID", "dbo.Skill");
            DropForeignKey("dbo.RequirementPosting", "Posting_ID", "dbo.Posting");
            DropForeignKey("dbo.RequirementPosting", "Requirement_ID", "dbo.Requirement");
            DropForeignKey("dbo.RequirementJob", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.RequirementJob", "Requirement_ID", "dbo.Requirement");
            DropForeignKey("dbo.QualificationPosting", "Posting_ID", "dbo.Posting");
            DropForeignKey("dbo.QualificationPosting", "Qualification_ID", "dbo.Qualification");
            DropForeignKey("dbo.QualificationJob", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.QualificationJob", "Qualification_ID", "dbo.Qualification");
            DropForeignKey("dbo.Posting", "JobID", "dbo.Job");
            DropForeignKey("dbo.Application", "PostingID", "dbo.Posting");
            DropForeignKey("dbo.Application", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.Application", "ApplicantID", "dbo.Applicant");
            DropForeignKey("dbo.ApplicantImage", "ApplicantImageID", "dbo.Applicant");
            DropIndex("dbo.SkillPosting", new[] { "Posting_ID" });
            DropIndex("dbo.SkillPosting", new[] { "Skill_ID" });
            DropIndex("dbo.SkillJob", new[] { "Job_ID" });
            DropIndex("dbo.SkillJob", new[] { "Skill_ID" });
            DropIndex("dbo.RequirementPosting", new[] { "Posting_ID" });
            DropIndex("dbo.RequirementPosting", new[] { "Requirement_ID" });
            DropIndex("dbo.RequirementJob", new[] { "Job_ID" });
            DropIndex("dbo.RequirementJob", new[] { "Requirement_ID" });
            DropIndex("dbo.QualificationPosting", new[] { "Posting_ID" });
            DropIndex("dbo.QualificationPosting", new[] { "Qualification_ID" });
            DropIndex("dbo.QualificationJob", new[] { "Job_ID" });
            DropIndex("dbo.QualificationJob", new[] { "Qualification_ID" });
            DropIndex("dbo.ExpiredPosting", new[] { "PostingID" });
            DropIndex("dbo.ExpiredPosting", new[] { "ApplicantID" });
            DropIndex("dbo.BestCandidate", new[] { "ApplicationID" });
            DropIndex("dbo.Archiveposting", new[] { "JobID" });
            DropIndex("dbo.Archiveposting", new[] { "SchoolID" });
            DropIndex("dbo.ArchiveApplication", new[] { "ApplicantID" });
            DropIndex("dbo.ArchiveApplication", new[] { "PostingID" });
            DropIndex("dbo.Appliedposting", new[] { "PostingID" });
            DropIndex("dbo.Appliedposting", new[] { "ApplicantID" });
            DropIndex("dbo.ApplicationComment", new[] { "ApplicationID" });
            DropIndex("dbo.Province", "IX_Unique_Province");
            DropIndex("dbo.FileContent", new[] { "FileContentID" });
            DropIndex("dbo.aFile", new[] { "ApplicantID" });
            DropIndex("dbo.SchoolLevel", "IX_Unique_LevelName");
            DropIndex("dbo.SchoolFamily", "IX_Unique_FamilyName");
            DropIndex("dbo.School", new[] { "SchoolFamilyID" });
            DropIndex("dbo.School", new[] { "CityID" });
            DropIndex("dbo.School", new[] { "SchoolLevelID" });
            DropIndex("dbo.SavedPosting", new[] { "Archiveposting_ID" });
            DropIndex("dbo.SavedPosting", new[] { "PostingID" });
            DropIndex("dbo.SavedPosting", new[] { "ApplicantID" });
            DropIndex("dbo.Skill", new[] { "Archiveposting_ID" });
            DropIndex("dbo.Skill", "IX_Unique_skill");
            DropIndex("dbo.Requirement", new[] { "Archiveposting_ID" });
            DropIndex("dbo.Requirement", "IX_Unique_Req");
            DropIndex("dbo.Qualification", new[] { "Archiveposting_ID" });
            DropIndex("dbo.Qualification", "IX_Unique_qual");
            DropIndex("dbo.Job", "IX_Unique_JobCode");
            DropIndex("dbo.Posting", new[] { "PostingTypesID" });
            DropIndex("dbo.Posting", new[] { "PostingStatusID" });
            DropIndex("dbo.Posting", new[] { "JobID" });
            DropIndex("dbo.Posting", new[] { "SchoolID" });
            DropIndex("dbo.Application", new[] { "Archiveposting_ID" });
            DropIndex("dbo.Application", new[] { "School_ID" });
            DropIndex("dbo.Application", new[] { "ApplicationStatusID" });
            DropIndex("dbo.Application", new[] { "ApplicantID" });
            DropIndex("dbo.Application", new[] { "PostingID" });
            DropIndex("dbo.Applicant", new[] { "CityID" });
            DropIndex("dbo.Applicant", new[] { "ProvinceID" });
            DropIndex("dbo.Applicant", "IX_Unique_Applicant_email");
            DropIndex("dbo.ApplicantImage", new[] { "ApplicantImageID" });
            DropTable("dbo.SkillPosting");
            DropTable("dbo.SkillJob");
            DropTable("dbo.RequirementPosting");
            DropTable("dbo.RequirementJob");
            DropTable("dbo.QualificationPosting");
            DropTable("dbo.QualificationJob");
            DropTable("dbo.ExpiredPosting");
            DropTable("dbo.BestCandidate");
            DropTable("dbo.Archiveposting");
            DropTable("dbo.ArchiveApplication");
            DropTable("dbo.Appliedposting");
            DropTable("dbo.ApplicationComment");
            DropTable("dbo.Province");
            DropTable("dbo.FileContent");
            DropTable("dbo.aFile");
            DropTable("dbo.SchoolLevel");
            DropTable("dbo.SchoolFamily");
            DropTable("dbo.City");
            DropTable("dbo.School");
            DropTable("dbo.SavedPosting");
            DropTable("dbo.PostingType");
            DropTable("dbo.PostingStatus");
            DropTable("dbo.Skill");
            DropTable("dbo.Requirement");
            DropTable("dbo.Qualification");
            DropTable("dbo.Job");
            DropTable("dbo.Posting");
            DropTable("dbo.ApplicationStatus");
            DropTable("dbo.Application");
            DropTable("dbo.Applicant");
            DropTable("dbo.ApplicantImage");
        }
    }
}
