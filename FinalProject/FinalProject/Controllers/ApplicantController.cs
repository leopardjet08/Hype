using FinalProject.DAL;
using FinalProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ApplicantController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();
        // GET: Applicant
        public ActionResult Index(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)

        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var applicants = from s in db.Applicants  select s;



            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                applicants = applicants.Where(p => p.FName.ToUpper().Contains(searchName.ToUpper()) || p.LName.ToUpper().Contains(searchName.ToUpper()) );
                ViewBag.Filtering = " in";
                ViewBag.searchName = searchName;
            }

            if (!String.IsNullOrEmpty(actionButton))
            {
                //Reset paging if ANY button was pushed
                page = 1;

                if (actionButton != "Search")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = String.IsNullOrEmpty(sortDirection) ? "desc" : "";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }

            if (sortField == "Name")//Sorting by Name
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applicants = applicants
                        .OrderBy(p => p.LName);
                }
                else
                {
                    applicants = applicants
                        .OrderByDescending(p => p.LName);
                }
            }
            
            else //By default sort by Name 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applicants = applicants
                        .OrderBy(p => p.FName);
                }
                else
                {
                    applicants = applicants
                        .OrderByDescending(p => p.FName);
                }
            }

            //Set sort for next time
            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            //number of data in the table
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(applicants.ToPagedList(pageNumber, pageSize));
        }
        //details controller
        public ActionResult Details(int? id, string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get all posting data
            Applicant applicant = db.Applicants
                .Where(p => p.ID == id).SingleOrDefault();
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Console.Write("something wrong");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", applicant.CityID);
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ID", "ProvinceName", applicant.ProvinceID);
            return View(applicant);
        }

        // POST: Applicants2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FName,MName,LName,EMail,Address,ProvinceID,CityID,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,RowVersion")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", applicant.CityID);
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ID", "ProvinceName", applicant.ProvinceID);
            return View(applicant);
        }


        public ActionResult Archive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        //For protection against hacker!!! 
        // POST: Postings/Delete
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            try
            {
                db.Applicants.Remove(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "You cannot delete a Applicant that has applications in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(applicant);

        }

        private void PopulateDropDownLists(Applicant applicant = null)
        {
            //ViewBag.ProvinceID = new SelectList(db.Provinces.OrderBy(p => p.ProvinceName), "ID", "ProvinceName", applicant?.ProvinceID);
            //ViewBag.CityID = new SelectList(db.Cities.OrderBy(p => p.CityName), "ID", "CityName", applicant?.CityID);
            

        }

    }
}