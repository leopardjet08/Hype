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
                applicants = applicants.Where(p => p.FullName.ToUpper().Contains(searchName.ToUpper()));
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
                        .OrderBy(p => p.FullName);
                }
                else
                {
                    applicants = applicants
                        .OrderByDescending(p => p.FullName);
                }
            }
            
            else //By default sort by Name 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applicants = applicants
                        .OrderBy(p => p.FullName);
                }
                else
                {
                    applicants = applicants
                        .OrderByDescending(p => p.FullName);
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
        public ActionResult Details()
        {
            return View();
        }

        private void PopulateDropDownLists(Applicant applicant = null)
        {
            
        }

    }
}