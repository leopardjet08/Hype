using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using FinalProject.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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
            string actionButton, string searchName, int? page)

        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var postings = from s in db.Postings where ((DateTime)s.ClosingDate >= DateTime.Today) && (s.PostingStatusID == 1) select s;



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

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///Expired index page

        // GET: Posting
        public ActionResult IndexExpired(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)

        {
            PopulateDropDownLists();
            ViewBag.Filtering = "";

            var postings = from s in db.Postings where ((DateTime)s.ClosingDate < DateTime.Today) || (s.PostingStatusID == 2) select s;



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

        /// ////////////////////////////////////////////////////////////////////////////////////////////



        public ActionResult CreateStart()
        {
            PopulateDropDownLists();
            return View("CreateStart");
        }
        //create controller
        public ActionResult Create(int? JobID)
        {
            Job job = db.Jobs
                .Where(p => p.ID == JobID)
                .SingleOrDefault();


            if (job == null)
            {
                ModelState.AddModelError("", "No Job to use as a Template");
                PopulateDropDownLists();
                return View("CreateStart");
            }
            //We have the positon to use as a template
            var posting = new Posting()
            {
                JobID = job.ID,
                Job = job,
                NumberOpen = 1,
                ClosingDate = DateTime.Today.AddDays(7),
                StartDate = DateTime.Today.AddDays(14),
                JobEndDate = DateTime.Today.AddDays(200),
                PostingDescription = job.JobSummary,
                Fte = 0.8,
                SchoolID = 1,
                JobCode = job.JobCode,
                Skills = job.Skills,
                Requirements = job.Requirements,
                Qualifications = job.Qualifications,
                SkillQualification = job.SkillQualification,
                PostingStatusID = 1
            };

            PopulateAssignedSkillData(posting);
            PopulateAssignedQualificationData(posting);
            PopulateAssignedRequirmentData(posting);

            PopulateDropDownLists();
            return View("Create", posting);
        }

        // POST: Postings/Create
        //For protection against hacker!!! 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumberOpen,ClosingDate,StartDate,JobEndDate,PostingDescription,Fte,SchoolID,JobID,JobCode,PostingStatusID")] Posting posting, string[] selectedSkills, string[] selectedRequirements, string[] selectedQualifications)
        {
            try
            {
                //Add the selected requirement
                if (selectedRequirements != null)
                {
                    posting.Requirements = new List<Requirement>();
                    foreach (var requirement in selectedRequirements)
                    {
                        var requirementToAdd = db.Requirements.Find(int.Parse(requirement));
                        posting.Requirements.Add(requirementToAdd);
                    }
                }

                //Add the selected skill
                if (selectedSkills != null)
                {
                    posting.Skills = new List<Skill>();
                    foreach (var skill in selectedSkills)
                    {
                        var skillToAdd = db.Skills.Find(int.Parse(skill));
                        posting.Skills.Add(skillToAdd);
                    }
                }

                //Add the selected qualification
                if (selectedQualifications != null)
                {
                    posting.Qualifications = new List<Qualification>();
                    foreach (var qualification in selectedQualifications)
                    {
                        var qualificationToAdd = db.Qualifications.Find(int.Parse(qualification));
                        posting.Qualifications.Add(qualificationToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    db.Postings.Add(posting);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            catch (NullReferenceException)
            {
                ModelState.AddModelError("", "Its a null daw yawa ");
            }

            PopulateDropDownLists(posting);

            PopulateAssignedRequirmentData(posting);
            PopulateAssignedSkillData(posting);
            PopulateAssignedQualificationData(posting);

            return View(posting);
        }


        //details controller
        public ActionResult Details(int? id, string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get all posting data
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

        public ActionResult Edit(int? id)
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

            if (posting.ClosingDate < DateTime.Today)
            {
                return RedirectToAction("Details", new { id = posting.ID });
            }
            PopulateDropDownLists(posting);

            PopulateAssignedSkillData(posting);
            PopulateAssignedQualificationData(posting);
            PopulateAssignedRequirmentData(posting);

            return View(posting);
        }

        // POST: Postings/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, Byte[] rowVersion, string[] selectedSkills, string[] selectedQuals, string[] selectedReqs)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var postingToUpdate = db.Postings
                .Where(p => p.ID == id).SingleOrDefault();

            if (TryUpdateModel(postingToUpdate, "",
               new string[] { "NumberOpen", "ClosingDate", "StartDate", "JobEndDate", "PostingDescription", "Fte", "SchoolID", "JobID", "JobCode" }))
            {
                try
                {
                    //ID,JobTitle,JobSummary

                    UpdatePostingQualifications(selectedQuals, postingToUpdate);
                    UpdatePostingRequirements(selectedReqs, postingToUpdate);
                    UpdatePostingSkills(selectedSkills, postingToUpdate);

                    db.Entry(postingToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException ex)// Added for concurrency
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Posting)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Posting was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Posting)databaseEntry.ToObject();
                        if (databaseValues.ClosingDate != clientValues.ClosingDate)
                            ModelState.AddModelError("ClosingDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.ClosingDate));
                        if (databaseValues.StartDate != clientValues.StartDate)
                            ModelState.AddModelError("StartDate", "Current Date: "
                                + String.Format("{0:d}", databaseValues.StartDate));
                        if (databaseValues.NumberOpen != clientValues.NumberOpen)
                            ModelState.AddModelError("NumberOpen", "Current Date: "
                                + databaseValues.NumberOpen);
                        if (databaseValues.JobEndDate != clientValues.JobEndDate)
                            ModelState.AddModelError("JobEndDate", "Current Date: "
                                + databaseValues.NumberOpen);
                        if (databaseValues.School.SchoolName != clientValues.School.SchoolName)
                            ModelState.AddModelError("SchooName", "Current name: "
                                + databaseValues.School.SchoolName);
                        if (databaseValues.Job.JobTitle != clientValues.Job.JobTitle)
                            ModelState.AddModelError("Job Title", "Current Job title: "
                                + databaseValues.Job.JobTitle);
                        if (databaseValues.PostingDescription != clientValues.PostingDescription)
                            ModelState.AddModelError("Posting Description", "Current Description: "
                                + databaseValues.PostingDescription);
                        if (databaseValues.Fte != clientValues.Fte)
                            ModelState.AddModelError("Fte", "Current Fte: "
                                + databaseValues.PostingDescription);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        postingToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            PopulateDropDownLists(postingToUpdate);

            PopulateAssignedSkillData(postingToUpdate);
            PopulateAssignedQualificationData(postingToUpdate);
            PopulateAssignedRequirmentData(postingToUpdate);

            return View(postingToUpdate);
        }


        public ActionResult Archive(int? id)
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

            return View(posting);
        }

        //For protection against hacker!!! 
        // POST: Postings/Delete
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(int? id)
        {
 

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var postingToUpdate = db.Postings
                .Where(p => p.ID == id).SingleOrDefault();

            if (TryUpdateModel(postingToUpdate, "",
               new string[] { "NumberOpen", "ClosingDate", "StartDate", "JobEndDate", "PostingDescription", "Fte", "SchoolID", "JobID", "JobCode", "PostingStatusID" }))
            {
                try
                {

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
               
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            PopulateDropDownLists(postingToUpdate);



            return View(postingToUpdate);

        }
        private void UpdatePostingSkills(string[] selectedSkills, Posting postingToUpdate)
        {
            if (selectedSkills == null)
            {
                postingToUpdate.Skills = new List<Skill>();
                return;
            }

            var selectedSkillsHS = new HashSet<string>(selectedSkills);
            var postingSkills = new HashSet<int>
                (postingToUpdate.Skills.Select(c => c.ID));//IDs of the currently selected skills
            foreach (var skill in db.Skills)
            {
                if (selectedSkillsHS.Contains(skill.ID.ToString()))
                {
                    if (!postingSkills.Contains(skill.ID))
                    {
                        postingToUpdate.Skills.Add(skill);
                    }
                }
                else
                {
                    if (postingSkills.Contains(skill.ID))
                    {
                        postingToUpdate.Skills.Remove(skill);
                    }
                }
            }
        }

        private void UpdatePostingRequirements(string[] selectedReqs, Posting postingToUpdate)
        {
            if (selectedReqs == null)
            {
                postingToUpdate.Requirements = new List<Requirement>();
                return;
            }

            var selectedRequirementHS = new HashSet<string>(selectedReqs);
            var postingRequirements = new HashSet<int>
                (postingToUpdate.Requirements.Select(c => c.ID));//IDs of the currently selected Reqs
            foreach (var reqs in db.Requirements)
            {
                if (selectedRequirementHS.Contains(reqs.ID.ToString()))
                {
                    if (!postingRequirements.Contains(reqs.ID))
                    {
                        postingToUpdate.Requirements.Add(reqs);
                    }
                }
                else
                {
                    if (postingRequirements.Contains(reqs.ID))
                    {
                        postingToUpdate.Requirements.Remove(reqs);
                    }
                }
            }
        }

        private void UpdatePostingQualifications(string[] selectedQuals, Posting postingToUpdate)
        {
            if (selectedQuals == null)
            {
                postingToUpdate.Qualifications = new List<Qualification>();
                return;
            }

            var selectedQualificationHS = new HashSet<string>(selectedQuals);
            var postingQualification = new HashSet<int>
                (postingToUpdate.Qualifications.Select(c => c.ID));//IDs of the currently selected Qualification
            foreach (var Qual in db.Qualifications)
            {
                if (selectedQualificationHS.Contains(Qual.ID.ToString()))
                {
                    if (!postingQualification.Contains(Qual.ID))
                    {
                        postingToUpdate.Qualifications.Add(Qual);
                    }
                }
                else
                {
                    if (postingQualification.Contains(Qual.ID))
                    {
                        postingToUpdate.Qualifications.Remove(Qual);
                    }
                }
            }
        }






        private SelectList JobSelectList(int? selectedID)
        {
            var dQuery = from d in db.Jobs.AsNoTracking()
                         orderby d.JobTitle
                         select d;
            return new SelectList(dQuery, "ID", "JobTitle", selectedID);
        }

        //private SelectList SchoolSelectedList(int? selectedID)
        //{
        //    var JetQuery = from d in db.Schools.Where(x => x.ID == selectedID)
        //                 select d.City;
        //    return new SelectList(JetQuery, "ID", "CityName", selectedID);
        //}

        //private SelectList ReqSelectedList(int? selectedID)
        //{
        //    var JetQuery = from e in db.Jobs from t in e.Requirements where e.ID == selectedID select t;

        //    return new SelectList(JetQuery, "ID", "RequirementName", selectedID);
        //}

        //private SelectList QualSelectedList(int? selectedID)
        //{
        //    var JetQuery = from e in db.Jobs from t in e.Qualifications where e.ID == selectedID select t;

        //    return new SelectList(JetQuery, "ID", "QualificationSet", selectedID);
        //}

        //private SelectList SkillSelectedList(int? selectedID)
        //{
        //    var JetQuery = from e in db.Jobs from t in e.Skills where e.ID == selectedID select t;

        //    return new SelectList(JetQuery, "ID", "SkillName", selectedID);
        //}
        //private SelectList DescSelectedList(int? selectedID)
        //{
        //    var JetQuery = from e in db.Jobs  where e.ID == selectedID select e;

        //    return new SelectList(JetQuery, "ID", "JobSummary", selectedID);
        //}


        private void PopulateDropDownLists(Posting posting = null)
        {
            ViewBag.JobID = new SelectList(db.Jobs.OrderBy(p => p.JobTitle), "ID", "JobTitle", posting?.JobID);
            //ViewBag.CityID = new SelectList( "", "");
            //ViewBag.Req = new SelectList("", "");
            //ViewBag.Qual = new SelectList("", "");
            //ViewBag.Skill = new SelectList("", "");
            ViewBag.SchoolID = new SelectList(db.Schools.OrderBy(p => p.SchoolName), "ID", "SchoolName", posting?.SchoolID);
        }

        [HttpGet]
        public ActionResult GetJobs(int? JobID)
        {
            SelectList jobs = JobSelectList(JobID);
            return Json(jobs, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetSchoolCity(int? SchoolID)
        //{

        //    SelectList schools = SchoolSelectedList(SchoolID);
        //    return Json(schools, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult Getreq(int? JobID)
        //{

        //    SelectList req = ReqSelectedList(JobID);
        //    return Json(req, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Getqual(int? JobID)
        //{

        //    SelectList req = QualSelectedList(JobID);
        //    return Json(req, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult Getskill(int? JobID)
        //{

        //    SelectList req = SkillSelectedList(JobID);
        //    return Json(req, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetDesc(int? JobID)
        //{

        //    SelectList req = DescSelectedList(JobID);
        //    return Json(req, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult GetAJob(int? ID)
        {
            try
            {
                Job job = db.Jobs
                    .Where(d => d.ID == ID)
                    .SingleOrDefault();

                //Build a string of html for the skills collection
                string Qualifications = "";
                foreach (var q in job.Qualifications)
                {
                    Qualifications += q.QualificationSet + "<br />";
                }
                //////////////////////////////////////////////////////////////////////////////////
                string Skills = "";
                foreach (var s in job.Skills)
                {
                    Skills += s.SkillName + "<br />";
                }
                /////////////////////////////////////////////////////////////////////////////////
                string Requirements = "";
                foreach (var r in job.Requirements)
                {
                    Requirements += r.RequirementName + "<br />";
                }



                //Using an annomous object as a DTO to avoid serialization errors
                var pos = new
                {
                    job.JobTitle,
                    job.JobSummary,
                    JobCode = job.JobCode,
                    Skills,
                    Qualifications,
                    Requirements,
                    job.SkillQualification

                };
                return Json(pos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult GetCity(int? ID)
        {
            try
            {
                School school = db.Schools
                    .Where(d => d.ID == ID)
                    .SingleOrDefault();


                var pos = new
                {
                    school.City.CityName,

                };
                return Json(pos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        private void PopulateAssignedSkillData(Posting posting)
        {
            var allSkills = db.Skills;
            var appSkills = new HashSet<int>(posting.Skills.Select(b => b.ID));
            var viewModel = new List<AssignedSkillVM>();
            foreach (var sk in allSkills)
            {
                viewModel.Add(new AssignedSkillVM
                {
                    SkillID = sk.ID,
                    SkillName = sk.SkillName,
                    Assigned = appSkills.Contains(sk.ID)
                });
            }
            ViewBag.Skills = viewModel;
        }

        private void PopulateAssignedRequirmentData(Posting posting)
        {
            var allRequirment = db.Requirements;
            var appRequirments = new HashSet<int>(posting.Requirements.Select(b => b.ID));
            var viewModel = new List<AssignedRequirmentVM>();
            foreach (var sk in allRequirment)
            {
                viewModel.Add(new AssignedRequirmentVM
                {
                    RequirmentID = sk.ID,
                    RequirementName = sk.RequirementName,
                    Assigned = appRequirments.Contains(sk.ID)
                });
            }
            ViewBag.Requirements = viewModel;
        }

        private void PopulateAssignedQualificationData(Posting posting)
        {
            var allQualifications = db.Qualifications;
            var appQualifications = new HashSet<int>(posting.Qualifications.Select(b => b.ID));
            var viewModel = new List<AssignedQualificationVM>();
            foreach (var sk in allQualifications)
            {
                viewModel.Add(new AssignedQualificationVM
                {
                    QualificationID = sk.ID,
                    QualificationSet = sk.QualificationSet,
                    Assigned = appQualifications.Contains(sk.ID)
                });
            }
            ViewBag.Qualifications = viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}