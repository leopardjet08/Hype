using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class PostingController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();


        // GET: Posting
        public ActionResult Index(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)
            
        {
            PopulateDropDownLists();
            ViewBag.Filtering = ""; 

            var postings = from s in db.Postings select s;

            

            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                postings = postings.Where(p => p.Job.JobTitle.ToUpper().Contains(searchName.ToUpper()));
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

            if (sortField == "Number of Openings")//Sorting by Number of opening
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    postings = postings
                        .OrderBy(p => p.NumberOpen);
                }
                else
                {
                    postings = postings
                        .OrderByDescending(p => p.NumberOpen);
                }
            }
            else if (sortField == "Closing Date")//Sorting by Closing Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    postings = postings
                        .OrderBy(p => p.ClosingDate);
                }
                else
                {
                    postings = postings
                        .OrderByDescending(p => p.ClosingDate);
                }
            }
            else if (sortField == "Start Date")//Sorting by Start Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    postings = postings
                        .OrderBy(p => p.StartDate);
                }
                else
                {
                    postings = postings
                        .OrderByDescending(p => p.StartDate);
                }
            }
            else if (sortField == "Posting Description") //Sorting by Applicant Name
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    postings = postings
                        .OrderBy(p => p.PostingDescription);
                }
                else   //Sorting by Posting description
                {
                    postings = postings
                        .OrderByDescending(p => p.PostingDescription);
                }
            }
            else //By default sort by Job title 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    postings = postings
                        .OrderBy(p => p.Job.JobTitle);
                }
                else
                {
                    postings = postings
                        .OrderByDescending(p => p.Job.JobTitle);
                }
            }

            //Set sort for next time
            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            //number of data in the table
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(postings.ToPagedList(pageNumber, pageSize));
        }

        //create controller
        public ActionResult Create()
        {
            PopulateDropDownLists();
            ViewBag.Message = "School by jet";

            var posting = new Posting();

            return View();
        }

        // POST: Postings/Create
        //For protection against hacker!!! 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumberOpen,ClosingDate,StartDate,PostingDescription,SchoolID,JobID")] Posting posting)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Postings.Add(posting);
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
            catch (Exception ) {
                ModelState.AddModelError("", "Wrong data. ");
            }

            PopulateDropDownLists(posting);
            return View(posting);
        }


        //details controller
        public ActionResult Details(int? id, string message)
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Posting posting = db.Postings
               .Where(p => p.ID == id).SingleOrDefault();

            if (posting == null)
            {
                return HttpNotFound();
            }

            if (posting.ClosingDate < DateTime.Today)
            {
                return RedirectToAction("Details", new { id = posting.ID });
            }
            PopulateDropDownLists(posting);
            return View(posting);
        }

        // POST: Postings/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, Byte[] rowVersion)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var postingToUpdate = db.Postings
                .Where(p => p.ID == id).SingleOrDefault();

            if (TryUpdateModel(postingToUpdate, "",
               new string[] { "NumberOpen", "ClosingDate", "StartDate", "SchoolID", "JobID","PostingDescription" }))
            {
                try
                {
                    db.Entry(postingToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException ex)// Added for concurrency
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Posting)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Posting was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Posting)databaseEntry.ToObject();
                        if (databaseValues.ClosingDate != clientValues.ClosingDate)
                            ModelState.AddModelError("ClosingDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.ClosingDate));
                        if (databaseValues.StartDate != clientValues.StartDate)
                            ModelState.AddModelError("StartDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.StartDate));
                        if (databaseValues.NumberOpen != clientValues.NumberOpen)
                            ModelState.AddModelError("NumberOpen", "Current value: "
                                + databaseValues.NumberOpen);
                        if (databaseValues.School.SchoolName != clientValues.School.SchoolName)
                            ModelState.AddModelError("SchooName", "Current name: "
                                + databaseValues.School.SchoolName);
                        if (databaseValues.Job.JobTitle != clientValues.Job.JobTitle)
                            ModelState.AddModelError("Job Title", "Current Job title: "
                                + databaseValues.Job.JobTitle);
                        if (databaseValues.PostingDescription != clientValues.PostingDescription)
                            ModelState.AddModelError("Posting Description", "Current Description: "
                                + databaseValues.PostingDescription);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        postingToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateDropDownLists(postingToUpdate);

            return View(postingToUpdate);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }
        
        //For protection against hacker!!! 
        // POST: Postings/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posting posting = db.Postings.Find(id);
            try
            {
                db.Postings.Remove(posting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "You cannot delete a Posting that has applications in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(posting);

        }

        private SelectList jobSelectList(int? selectedID)
        {
            var dQuery = from d in db.Jobs.AsNoTracking()
                         orderby d.JobTitle
                         select d;
            return new SelectList(dQuery, "ID", "JobTitle", selectedID);
        }

        private SelectList SchoolSelectedList(int? selectedID)
        {
            var JetQuery = from d in db.Postings.Where(x => x.School.ID == selectedID)
                         select d.School.City;
            return new SelectList(JetQuery, "ID", "CityName", selectedID);
        }

        private void PopulateDropDownLists(Posting posting = null)
        {
            ViewBag.JobID = new SelectList(db.Jobs.OrderBy(p => p.JobTitle), "ID", "JobTitle", posting?.JobID);
            ViewBag.CityID = new SelectList( "", "");
            ViewBag.SchoolID = new SelectList(db.Schools.OrderBy(p => p.SchoolName), "ID", "SchoolName", posting?.SchoolID);
        }

        [HttpGet]
        public ActionResult GetJobs(int? JobID)
        {
            SelectList jobs = jobSelectList(JobID);
            return Json(jobs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSchoolCity(int? SchoolID)
        {

            SelectList schools = SchoolSelectedList(SchoolID);
            return Json(schools, JsonRequestBehavior.AllowGet);
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