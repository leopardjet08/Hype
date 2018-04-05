using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FinalProject.Controllers
{
    public class ApplyController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Apply
        public ActionResult Index()
        {
            var applications = db.applications.Include(a => a.Applicant).Include(a => a.ApplicationStatus).Include(a => a.Posting);
            return View(applications.ToList());
        }

        // GET: Apply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        //details controller
        public ActionResult AppliedView(int? id, string message)
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

            ViewBag.Message = message;
            ViewBag.Closed = posting.ClosingDate < DateTime.Today;
            return View(posting);

        }
        // GET: Apply/Create
        public ActionResult Create(int? PostingID)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            Posting posting = db.Postings
               .Where(p => p.ID == PostingID)
               .SingleOrDefault();

            //if (User.Identity.IsAuthenticated)
            //{
            //     Membership.GetUser().Email;
            //}
            ViewBag.name = User.Identity.Name;

            Applicant q = db.Applicants
               .Where(p => p.EMail == User.Identity.Name)
               .SingleOrDefault();



            if (posting == null)
            {
                ModelState.AddModelError("", "Something got wrong");
                return View("AppliedView");
            }

            if (q == null)
            {
                ModelState.AddModelError("", "You need to login First");
                return RedirectToAction("Login", "Account");
            }


            var application = new Application()
            {
                PostingID = posting.ID,
                Posting =posting,
                Applicant=q,
                ApplicantID = q.ID
            };

            return View("Create",application);
        }

        // POST: Apply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostingID,ApplicantID,ApplicationStatusID")] Application application)
        {
            Applicant q = db.Applicants
               .Where(p => p.EMail == User.Identity.Name)
               .SingleOrDefault();



            if (ModelState.IsValid)
            {
                db.applications.Add(application);
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Submit Application Successfully');</script>";
                return RedirectToAction("Index","Home");
            }

            ViewBag.ApplicantID = new SelectList(db.Applicants, "ID", "FName", q.ID);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ID", "Status", application.ApplicationStatusID);
            ViewBag.PostingID = new SelectList(db.Postings, "ID", "PostingDescription", application.PostingID);

            TempData["msg"] = "<script>alert('Submit Application Failed');</script>";
            return View(application);
        }

        // GET: Apply/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.applications.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,PostingID,ApplicantID,SubmissionDate,ApplicationStatusID,Comment")] Application application)
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
            Application application = db.applications.Find(id);
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
            Application application = db.applications.Find(id);
            db.applications.Remove(application);
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
