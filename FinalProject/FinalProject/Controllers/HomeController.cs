using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using PagedList;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        public ActionResult Index(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)

        {
            
            ViewBag.Filtering = "";

            var postings = from s in db.Postings where ((DateTime)s.ClosingDate >= DateTime.Today) select s;

            ViewBag.ShowList = false;

            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                ViewBag.ShowList = true;
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

            if (!String.IsNullOrEmpty(actionButton))
            {
                

                if (actionButton == "Search")//Change of sort is requested
                {
                    if (searchName == "") //Reverse order on same field
                    {
                        ViewBag.ShowList = true;
                    }
                    
                }
            }

            postings = postings
                         .OrderBy(p => p.Job.JobTitle);

            //Set sort for next time
            ViewBag.sortField = sortField;
            

            //number of data in the table
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(postings.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
    }
}