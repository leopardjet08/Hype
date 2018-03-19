using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models.DataModel;

namespace FinalProject.Controllers
{
    public class RequirementsController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Requirements
        public ActionResult Index()
        {
            return View(db.Requirements.ToList());
        }

        // GET: Requirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requirement requirement = db.Requirements.Find(id);
            if (requirement == null)
            {
                return HttpNotFound();
            }
            return View(requirement);
        }

        // GET: Requirements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequirementName")] Requirement requirement)
        {
            if (ModelState.IsValid)
            {
                db.Requirements.Add(requirement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requirement);
        }

        // GET: Requirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requirement requirement = db.Requirements.Find(id);
            if (requirement == null)
            {
                return HttpNotFound();
            }
            return View(requirement);
        }

        // POST: Requirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RequirementName")] Requirement requirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requirement);
        }

        // GET: Requirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requirement requirement = db.Requirements.Find(id);
            if (requirement == null)
            {
                return HttpNotFound();
            }
            return View(requirement);
        }

        // POST: Requirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requirement requirement = db.Requirements.Find(id);
            db.Requirements.Remove(requirement);
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
