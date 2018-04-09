using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ArchiveController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();


        public ActionResult IndexApplications(string sortDirection, string sortField,
           string actionButton, string searchName, int? page)
        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var application = from s in db.ArchiveApplications select s;



            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                application = application.Where(p => p.Applications.Posting.Job.JobTitle.ToUpper().Contains(searchName.ToUpper()));
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
                        .OrderBy(p => p.Applications.Posting.Job.JobTitle);
                }
                else
                {
                    application = application
                         .OrderByDescending(p => p.Applications.Posting.Job.JobTitle);
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

            

            else //By default sort by Job title 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    application = application
                        .OrderBy(p => p.Applications.Posting.Job.JobTitle);
                }
                else
                {
                    application = application
                         .OrderByDescending(p => p.Applications.Posting.Job.JobTitle);
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

        public ActionResult IndexPostings(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)

        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var archivePostings = from s in db.Archivepostings select s;



            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                archivePostings = archivePostings.Where(p => p.Job.JobTitle.ToUpper().Contains(searchName.ToUpper()));
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
                    archivePostings = archivePostings
                        .OrderBy(p => p.NumberOpen);
                }
                else
                {
                    archivePostings = archivePostings
                        .OrderByDescending(p => p.NumberOpen);
                }
            }
            else if (sortField == "Closing Date")//Sorting by Closing Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    archivePostings = archivePostings
                        .OrderBy(p => p.ClosingDate);
                }
                else
                {
                    archivePostings = archivePostings
                        .OrderByDescending(p => p.ClosingDate);
                }
            }
            else if (sortField == "Start Date")//Sorting by Start Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    archivePostings = archivePostings
                        .OrderBy(p => p.StartDate);
                }
                else
                {
                    archivePostings = archivePostings
                        .OrderByDescending(p => p.StartDate);
                }
            }
            else if (sortField == "Posting Description") //Sorting by Applicant Name
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    archivePostings = archivePostings
                        .OrderBy(p => p.PostingDescription);
                }
                else   //Sorting by Posting description
                {
                    archivePostings = archivePostings
                        .OrderByDescending(p => p.PostingDescription);
                }
            }
            else //By default sort by Job title 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    archivePostings = archivePostings
                        .OrderBy(p => p.Job.JobTitle);
                }
                else
                {
                    archivePostings = archivePostings
                        .OrderByDescending(p => p.Job.JobTitle);
                }
            }

            //Set sort for next time
            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            //number of data in the table
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(archivePostings.ToPagedList(pageNumber, pageSize));
        }

        private void PopulateDropDownLists(Application app = null)
        {

        }
        public ActionResult ArchivePostingDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get all posting data
            Archiveposting archiveposting = db.Archivepostings
                .Where(p => p.ID == id).SingleOrDefault();

            if (archiveposting == null)
            {
                return HttpNotFound();
            }
            return View(archiveposting);
        }


    }
}