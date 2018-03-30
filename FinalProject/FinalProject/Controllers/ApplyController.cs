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
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.Applicant).Include(a => a.ApplicationStatus).Include(a => a.Posting);
            return View(applications.ToList());
        }

        // GET: Apply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Apply/Create
        public ActionResult Create()
        {
            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName");
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status");
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription");
            return View();
        }

        // POST: Apply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostingID,ApplicantID,SubmissionDate,ApplicationStatusID")] Application application)
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

        // GET: Apply/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", application.ApplicantID);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);
            return View(application);
        }

        // POST: Apply/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostingID,ApplicantID,SubmissionDate,ApplicationStatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", application.ApplicantID);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);
            return View(application);
        }

        // GET: Apply/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Apply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
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
