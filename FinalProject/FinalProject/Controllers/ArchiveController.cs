﻿using FinalProject.DAL;
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
        // GET: Archive
        /////////////////Application details Old Index////////////////////////////////////////////////////////////

        public ActionResult IndexApplications(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)
        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var application = from s in db.Applications select s;



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

        public ActionResult IndexPostings()
        {
            return View();
        }

        private void PopulateDropDownLists(Application app = null)
        {

        }

    }
}