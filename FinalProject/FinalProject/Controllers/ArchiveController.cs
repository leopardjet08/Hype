using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ArchiveController : Controller
    {
        // GET: Archive
        public ActionResult IndexApplications()
        {
            return View();
        }

        public ActionResult IndexPostings()
        {
            return View();
        }

    }
}