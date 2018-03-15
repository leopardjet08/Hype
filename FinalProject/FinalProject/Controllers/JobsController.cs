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
    public class JobsController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Jobs
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            PopulateDropDownLists();
            //Add all (unchecked) Skills to ViewBag
            var job = new Job();
            job.Requirements = new List<Requirement>();
            PopulateAssignedRequirementData(job);
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,JobTitle,JobSummary,SkillQualification")] Job job, string[] selectedRequirments)
        {
            
                try
                {
                    //Add the selected skills
                    if (selectedRequirments != null)
                    {
                        job.Requirements = new List<Requirement>();
                        foreach (var requirment in selectedRequirments)
                        {
                            var requirmentToAdd = db.Requirements.Find(int.Parse(requirment));
                            job.Requirements.Add(requirmentToAdd);
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        db.Jobs.Add(job);
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

                PopulateDropDownLists(job);
                return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,JobTitle,JobSummary")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateAssignedRequirementData(Job job)
        {
            var allRequirments = db.Requirements;
            var appRequirments = new HashSet<int>(job.Requirements.Select(b => b.ID));
            var viewModel = new List<AssignedRequirmentVM>();
            foreach (var sr in allRequirments)
            {
                viewModel.Add(new AssignedRequirmentVM
                {
                    RequirmentID = sr.ID,
                    RequirementName = sr.RequirementName,
                    Assigned = appRequirments.Contains(sr.ID)
                });
            }
            ViewBag.Skills = viewModel;
        }

        private void PopulateDropDownLists(Job job = null)
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
