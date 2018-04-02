using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ApplyController : Controller
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
            return View(posting);
        }

        //details controller



        // GET: Apply/Create
        public ActionResult Create(int? PostingID)
        {

            Posting posting = db.Postings
               .Where(p => p.ID == PostingID)
               .SingleOrDefault();

            //Applicant applicant

            if (posting == null)
            {
                ModelState.AddModelError("", "No Job to use as a Template");
                PopulateDropDownLists();
                return View("Index");
            }

            var application = new Application()
            {
                PostingID = posting.ID,
                ApplicantID = 1,
                ApplicationStatusID =1
                
            };

            return View("Create", posting);
        }

        // POST: Apply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostingID,ApplicantID,ApplicationStatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", application.ApplicantID);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);
            return View(application);
        }

        private void PopulateDropDownLists(Posting posting = null)
        {
           
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
