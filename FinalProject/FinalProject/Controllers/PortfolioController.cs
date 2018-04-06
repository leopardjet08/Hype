﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FinalProject.Controllers
{
    public class PortfolioController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();
        // GET: Portfolio
        public ActionResult Index()
        {
            var applicant = db.Applicants
                .Include(a =>a.Files)
                .Include(a => a.SavedPostings)
                .Include(a => a.Appliedpostings)
                .Include(a=>a.ExpiredPostings)
                .Where(a =>a.EMail == User.Identity.Name).SingleOrDefault();

            if (applicant == null) {
                ModelState.AddModelError("", "You need to login First");
                return RedirectToAction("Login", "Account");
            }

            return View(applicant);
        }


        public ActionResult Delete(int id)
        {
            Appliedposting applied = db.Appliedpostings.Find(id);
            db.Appliedpostings.Remove(applied);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}