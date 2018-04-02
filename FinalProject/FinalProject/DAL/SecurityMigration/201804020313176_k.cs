namespace FinalProject.DAL.SecurityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class k : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
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
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceID, cascadeDelete: true)
                .Index(t => t.EMail, unique: true, name: "IX_Unique_Applicant_email")
                .Index(t => t.ProvinceID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.ApplicantImages",
                c => new
                    {
                        ApplicantImageID = c.Int(nullable: false),
                        ImageContent = c.Binary(),
                        ImageMimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ApplicantImageID)
                .ForeignKey("dbo.Applicants", t => t.ApplicantImageID)
                .Index(t => t.ApplicantImageID);
            
            CreateTable(
                "dbo.Applications",
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
                .ForeignKey("dbo.Applicants", t => t.ApplicantID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.PostingID, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.School_ID)
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
                "dbo.Postings",
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
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.PostingStatus", t => t.PostingStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.SchoolID, cascadeDelete: true)
                .Index(t => t.SchoolID)
                .Index(t => t.JobID)
                .Index(t => t.PostingStatusID);
            
            CreateTable(
                "dbo.Jobs",
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
                "dbo.Qualifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QualificationSet = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.QualificationSet, unique: true, name: "IX_Unique_qual");
            
            CreateTable(
                "dbo.Requirements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequirementName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.RequirementName, unique: true, name: "IX_Unique_Req");
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.SkillName, unique: true, name: "IX_Unique_skill");
            
            CreateTable(
                "dbo.PostingStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SavedPostings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        PostingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicants", t => t.ApplicantID, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.PostingID, cascadeDelete: true)
                .Index(t => t.ApplicantID)
                .Index(t => t.PostingID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(nullable: false, maxLength: 80),
                        SchoolLevelID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SchoolFamilyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: false)
                .ForeignKey("dbo.SchoolFamilies", t => t.SchoolFamilyID, cascadeDelete: true)
                .ForeignKey("dbo.SchoolLevels", t => t.SchoolLevelID, cascadeDelete: true)
                .Index(t => t.SchoolLevelID)
                .Index(t => t.CityID)
                .Index(t => t.SchoolFamilyID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.CityName, unique: true, name: "IX_Unique_City");
            
            CreateTable(
                "dbo.SchoolFamilies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.FamilyName, unique: true, name: "IX_Unique_FamilyName");
            
            CreateTable(
                "dbo.SchoolLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.LevelName, unique: true, name: "IX_Unique_LevelName");
            
            CreateTable(
                "dbo.aFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fileName = c.String(maxLength: 256),
                        Description = c.String(maxLength: 256),
                        ApplicantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Applicants", t => t.ApplicantID, cascadeDelete: true)
                .Index(t => t.ApplicantID);
            
            CreateTable(
                "dbo.FileContents",
                c => new
                    {
                        FileContentID = c.Int(nullable: false),
                        Content = c.Binary(),
                        MimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FileContentID)
                .ForeignKey("dbo.aFiles", t => t.FileContentID)
                .Index(t => t.FileContentID);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ProvinceName, unique: true, name: "IX_Unique_Province");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QualificationJobs",
                c => new
                    {
                        Qualification_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Qualification_ID, t.Job_ID })
                .ForeignKey("dbo.Qualifications", t => t.Qualification_ID, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Qualification_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.QualificationPostings",
                c => new
                    {
                        Qualification_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Qualification_ID, t.Posting_ID })
                .ForeignKey("dbo.Qualifications", t => t.Qualification_ID, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Qualification_ID)
                .Index(t => t.Posting_ID);
            
            CreateTable(
                "dbo.RequirementJobs",
                c => new
                    {
                        Requirement_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Requirement_ID, t.Job_ID })
                .ForeignKey("dbo.Requirements", t => t.Requirement_ID, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Requirement_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.RequirementPostings",
                c => new
                    {
                        Requirement_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Requirement_ID, t.Posting_ID })
                .ForeignKey("dbo.Requirements", t => t.Requirement_ID, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Requirement_ID)
                .Index(t => t.Posting_ID);
            
            CreateTable(
                "dbo.SkillJobs",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Job_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Job_ID })
                .ForeignKey("dbo.Skills", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.SkillPostings",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Posting_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Posting_ID })
                .ForeignKey("dbo.Skills", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.Posting_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Posting_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Applicants", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.FileContents", "FileContentID", "dbo.aFiles");
            DropForeignKey("dbo.aFiles", "ApplicantID", "dbo.Applicants");
            DropForeignKey("dbo.Schools", "SchoolLevelID", "dbo.SchoolLevels");
            DropForeignKey("dbo.Schools", "SchoolFamilyID", "dbo.SchoolFamilies");
            DropForeignKey("dbo.Postings", "SchoolID", "dbo.Schools");
            DropForeignKey("dbo.Schools", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Applicants", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Applications", "School_ID", "dbo.Schools");
            DropForeignKey("dbo.SavedPostings", "PostingID", "dbo.Postings");
            DropForeignKey("dbo.SavedPostings", "ApplicantID", "dbo.Applicants");
            DropForeignKey("dbo.Postings", "PostingStatusID", "dbo.PostingStatus");
            DropForeignKey("dbo.SkillPostings", "Posting_ID", "dbo.Postings");
            DropForeignKey("dbo.SkillPostings", "Skill_ID", "dbo.Skills");
            DropForeignKey("dbo.SkillJobs", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.SkillJobs", "Skill_ID", "dbo.Skills");
            DropForeignKey("dbo.RequirementPostings", "Posting_ID", "dbo.Postings");
            DropForeignKey("dbo.RequirementPostings", "Requirement_ID", "dbo.Requirements");
            DropForeignKey("dbo.RequirementJobs", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.RequirementJobs", "Requirement_ID", "dbo.Requirements");
            DropForeignKey("dbo.QualificationPostings", "Posting_ID", "dbo.Postings");
            DropForeignKey("dbo.QualificationPostings", "Qualification_ID", "dbo.Qualifications");
            DropForeignKey("dbo.QualificationJobs", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.QualificationJobs", "Qualification_ID", "dbo.Qualifications");
            DropForeignKey("dbo.Postings", "JobID", "dbo.Jobs");
            DropForeignKey("dbo.Applications", "PostingID", "dbo.Postings");
            DropForeignKey("dbo.Applications", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.Applications", "ApplicantID", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantImages", "ApplicantImageID", "dbo.Applicants");
            DropIndex("dbo.SkillPostings", new[] { "Posting_ID" });
            DropIndex("dbo.SkillPostings", new[] { "Skill_ID" });
            DropIndex("dbo.SkillJobs", new[] { "Job_ID" });
            DropIndex("dbo.SkillJobs", new[] { "Skill_ID" });
            DropIndex("dbo.RequirementPostings", new[] { "Posting_ID" });
            DropIndex("dbo.RequirementPostings", new[] { "Requirement_ID" });
            DropIndex("dbo.RequirementJobs", new[] { "Job_ID" });
            DropIndex("dbo.RequirementJobs", new[] { "Requirement_ID" });
            DropIndex("dbo.QualificationPostings", new[] { "Posting_ID" });
            DropIndex("dbo.QualificationPostings", new[] { "Qualification_ID" });
            DropIndex("dbo.QualificationJobs", new[] { "Job_ID" });
            DropIndex("dbo.QualificationJobs", new[] { "Qualification_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Provinces", "IX_Unique_Province");
            DropIndex("dbo.FileContents", new[] { "FileContentID" });
            DropIndex("dbo.aFiles", new[] { "ApplicantID" });
            DropIndex("dbo.SchoolLevels", "IX_Unique_LevelName");
            DropIndex("dbo.SchoolFamilies", "IX_Unique_FamilyName");
            DropIndex("dbo.Cities", "IX_Unique_City");
            DropIndex("dbo.Schools", new[] { "SchoolFamilyID" });
            DropIndex("dbo.Schools", new[] { "CityID" });
            DropIndex("dbo.Schools", new[] { "SchoolLevelID" });
            DropIndex("dbo.SavedPostings", new[] { "PostingID" });
            DropIndex("dbo.SavedPostings", new[] { "ApplicantID" });
            DropIndex("dbo.Skills", "IX_Unique_skill");
            DropIndex("dbo.Requirements", "IX_Unique_Req");
            DropIndex("dbo.Qualifications", "IX_Unique_qual");
            DropIndex("dbo.Jobs", "IX_Unique_JobCode");
            DropIndex("dbo.Postings", new[] { "PostingStatusID" });
            DropIndex("dbo.Postings", new[] { "JobID" });
            DropIndex("dbo.Postings", new[] { "SchoolID" });
            DropIndex("dbo.Applications", new[] { "School_ID" });
            DropIndex("dbo.Applications", new[] { "ApplicationStatusID" });
            DropIndex("dbo.Applications", "IX_Unique_Application");
            DropIndex("dbo.Applications", "IX_Unique_Posting");
            DropIndex("dbo.ApplicantImages", new[] { "ApplicantImageID" });
            DropIndex("dbo.Applicants", new[] { "CityID" });
            DropIndex("dbo.Applicants", new[] { "ProvinceID" });
            DropIndex("dbo.Applicants", "IX_Unique_Applicant_email");
            DropTable("dbo.SkillPostings");
            DropTable("dbo.SkillJobs");
            DropTable("dbo.RequirementPostings");
            DropTable("dbo.RequirementJobs");
            DropTable("dbo.QualificationPostings");
            DropTable("dbo.QualificationJobs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Provinces");
            DropTable("dbo.FileContents");
            DropTable("dbo.aFiles");
            DropTable("dbo.SchoolLevels");
            DropTable("dbo.SchoolFamilies");
            DropTable("dbo.Cities");
            DropTable("dbo.Schools");
            DropTable("dbo.SavedPostings");
            DropTable("dbo.PostingStatus");
            DropTable("dbo.Skills");
            DropTable("dbo.Requirements");
            DropTable("dbo.Qualifications");
            DropTable("dbo.Jobs");
            DropTable("dbo.Postings");
            DropTable("dbo.ApplicationStatus");
            DropTable("dbo.Applications");
            DropTable("dbo.ApplicantImages");
            DropTable("dbo.Applicants");
        }
    }
}
