using FinalProject.DAL;
using FinalProject.Models;
using PagedList;
using System;
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
            string actionButton, string searchName, string[] selectedSkills, int? page)
            
        {
            PopulateDropDownLists();
            ViewBag.Filtering = ""; //Assume not filtering

            var postings = from s in db.Postings select s;

            

            //Add as many filters as you want
            if (!String.IsNullOrEmpty(searchName))
            {
                postings = postings.Where(p => p.Job.JobTitle.ToUpper().Contains(searchName.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.searchName = searchName;
            }

            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
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

            if (sortField == "Number of Openings")//Sorting by Applicant Name
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
            else if (sortField == "Closing Date")
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
            else if (sortField == "Start Date")
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
            } else if (sortField == "Posting Description") {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    postings = postings
                        .OrderBy(p => p.PostingDescription);
                }
                else
                {
                    postings = postings
                        .OrderByDescending(p => p.PostingDescription);
                }
            }
            else //By default sort by Position NAME
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

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(postings.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Details(int? id, string message)
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
            ViewBag.Message = message;
            ViewBag.Closed = posting.ClosingDate < DateTime.Today;
            return View(posting);

        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        private void PopulateDropDownLists(Posting posting = null)
        {
            ViewBag.JobID = new SelectList(db.Jobs.OrderBy(p => p.JobTitle), "ID", "Title", posting?.JobID);
        }
    }
}