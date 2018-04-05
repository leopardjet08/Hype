using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using FinalProject.ViewModels;

namespace FinalProject.Controllers
{
    public class AapplyController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Apply
        public ActionResult Index(int? id, string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get all posting data
            Posting posting = db.Postings
                .Where(p => p.ID == id).SingleOrDefault();
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View("Index",posting);
        }



        //// GET: Apply/Create
        //public ActionResult Create(int? PostingID)
        //{

        //    Posting posting = db.Postings
        //       .Where(p => p.ID == PostingID)
        //       .SingleOrDefault();

        //    //Applicant applicant

        //    if (posting == null)
        //    {
        //        ModelState.AddModelError("", "No Job to use as a Template");
        //        PopulateDropDownLists();
        //        return View("Index");
        //    }

        //    var application = new Application()
        //    {
        //        PostingID = posting.ID,
        //        ApplicantID = 1,
        //        ApplicationStatusID =100
                
        //    };

        //    PopulateAssignedSkillData(posting);
        //    PopulateAssignedQualificationData(posting);
        //    PopulateAssignedRequirmentData(posting);

        //    return View("Create", posting);
        //}

        //// POST: Apply/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,PostingID,ApplicantID,ApplicationStatusID")] Application application)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Applications.Add(application);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", application.ApplicantID);
        //    ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
        //    ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);
        //    return View(application);
        //}


        // GET: Jobs/Create
        public ActionResult Createe(int? PostingID)
        {

            Posting posting = db.Postings
               .Where(p => p.ID == PostingID)
               .SingleOrDefault();

            //Applicant applicant

            if (posting == null)
            {
                ModelState.AddModelError("", "No Posting to use as a Template");
                PopulateDropDownLists();
                return View("Index");
            }

            var application = new Application()
            {
                PostingID = posting.ID,
                ApplicantID = 1,
                ApplicationStatusID = 1

            };

            PopulateAssignedSkillData(posting);
            PopulateAssignedQualificationData(posting);
            PopulateAssignedRequirmentData(posting);

            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", application.ApplicantID);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);

            return View("Create", posting);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createe([Bind(Include = "ID,PostingID,ApplicantID,ApplicationStatusID")] Application application)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    db.Applications.Add(application);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", application.ApplicantID);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);

            return View(application);
        }




        private void PopulateDropDownLists(Application application = null)
        {
            ViewBag.PostingID = new SelectList(db.Jobs.OrderBy(p => p.Postings), "ID", "JobTitle", application?.PostingID);
            ViewBag.ApplicantID = new SelectList(db.Schools.OrderBy(p => p.SchoolName), "ID", "SchoolName", application?.PostingID);

        }

        private void PopulateAssignedSkillData(Posting posting)
        {
            var allSkills = db.Skills;
            var appSkills = new HashSet<int>(posting.Skills.Select(b => b.ID));
            var viewModel = new List<AssignedSkillVM>();
            foreach (var sk in allSkills)
            {
                viewModel.Add(new AssignedSkillVM
                {
                    SkillID = sk.ID,
                    SkillName = sk.SkillName,
                    Assigned = appSkills.Contains(sk.ID)
                });
            }
            ViewBag.Skills = viewModel;
        }

        private void PopulateAssignedRequirmentData(Posting posting)
        {
            var allRequirment = db.Requirements;
            var appRequirments = new HashSet<int>(posting.Requirements.Select(b => b.ID));
            var viewModel = new List<AssignedRequirmentVM>();
            foreach (var sk in allRequirment)
            {
                viewModel.Add(new AssignedRequirmentVM
                {
                    RequirmentID = sk.ID,
                    RequirementName = sk.RequirementName,
                    Assigned = appRequirments.Contains(sk.ID)
                });
            }
            ViewBag.Requirements = viewModel;
        }

        private void PopulateAssignedQualificationData(Posting posting)
        {
            var allQualifications = db.Qualifications;
            var appQualifications = new HashSet<int>(posting.Qualifications.Select(b => b.ID));
            var viewModel = new List<AssignedQualificationVM>();
            foreach (var sk in allQualifications)
            {
                viewModel.Add(new AssignedQualificationVM
                {
                    QualificationID = sk.ID,
                    QualificationSet = sk.QualificationSet,
                    Assigned = appQualifications.Contains(sk.ID)
                });
            }
            ViewBag.Qualifications = viewModel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
