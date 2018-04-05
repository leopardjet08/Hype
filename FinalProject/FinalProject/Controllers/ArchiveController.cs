using FinalProject.DAL;
using FinalProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (sortField == "Job Title")//Sorting by Job title
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

            else if (sortField == "Employer")//Sorting by School
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

        public ActionResult IndexPostings(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)

        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var postings = from s in db.Postings where ((DateTime)s.ClosingDate < DateTime.Today) select s;



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

        private void PopulateDropDownLists(Application app = null)
        {

        }

    }
}