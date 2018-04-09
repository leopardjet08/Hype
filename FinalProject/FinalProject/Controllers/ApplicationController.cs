using FinalProject.DAL;
using FinalProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ApplicationController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Application
        public ActionResult Index(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)
        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var application = from s in db.applications select s;



            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                application = application.Where(p => p.Posting.Job.JobTitle.ToUpper().Contains(searchName.ToUpper()));
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

            if (sortField == "Job Applied For")//Sorting by Job title
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    application = application
                        .OrderBy(p => p.Posting.Job.JobTitle);
                }
                else
                {
                    application = application
                         .OrderByDescending(p => p.Posting.Job.JobTitle);
                }
            }
            else if (sortField == "Submission Date")
            {
                if (sortField == "Submission Date")//Sorting by Submission DATE
                {
                    if (String.IsNullOrEmpty(sortDirection))
                    {
                        application = application
                            .OrderBy(p => p.SubmissionDate);
                    }
                    else
                    {
                        application = application
                             .OrderByDescending(p => p.SubmissionDate);
                    }
                }
            }

            else if (sortField == "School")//Sorting by School
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    application = application
                        .OrderBy(p => p.Posting.School.SchoolName);
                }
                else

                    application = application
                        .OrderByDescending(p => p.Posting.School.SchoolName);


            }

            else //By default sort by Job title 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    application = application
                        .OrderBy(p => p.Posting.Job.JobTitle);
                }
                else
                {
                    application = application
                         .OrderByDescending(p => p.Posting.Job.JobTitle);
                }
            }


            //Set sort for next time
            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            //number of data in the table
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(application.ToPagedList(pageNumber, pageSize));
        }




        public ActionResult Details(int? id, string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get all posting data
            Application application = db.applications
                .Where(p => p.ID == id).SingleOrDefault();
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = message;
            return View(application);

        }
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

        //For protection against hacker!!! 
        // POST: Application/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.applications.Find(id);
            try
            {
                db.applications.Remove(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "You cannot delete a Application.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(application);

        }

        private void PopulateDropDownLists(Application app = null)
        {

        }

        // POST: Postings/Edit/5
        public ActionResult AcceptEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ApplicationtoUpdate = db.applications
                .Where(p => p.ID == id).SingleOrDefault();

            ApplicationtoUpdate.ApplicationStatusID = 3;
            db.SaveChanges();
            return RedirectToAction("Index"); 
                
          }

        public ActionResult DeclineEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ApplicationtoUpdate = db.applications
                .Where(p => p.ID == id).SingleOrDefault();

            ApplicationtoUpdate.ApplicationStatusID = 2;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}