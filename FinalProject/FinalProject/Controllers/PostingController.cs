using FinalProject.DAL;
using FinalProject.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class PostingController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();


        // GET: Posting
        public ActionResult Index(int? pageNumber)
            
        {
            var postings = from s in db.Postings select s;

            postings = postings.OrderBy(s => s.NumberOpen);

            ViewBag.Count = postings.Count();

            return View(postings.ToList().ToPagedList(pageNumber ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
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