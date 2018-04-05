using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using FinalProject.ViewModels;

namespace FinalProject.Controllers
{
    public class JobsController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Jobs
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            PopulateDropDownLists();
            //Add all (unchecked) Requirement to ViewBag
            var job = new Job();


            job.Requirements = new List<Requirement>();
            PopulateAssignedRequirmentData(job);

            job.Skills = new List<Skill>();
            PopulateAssignedSkillData(job);

            job.Qualifications = new List<Qualification>();
            PopulateAssignedQualificationData(job);

            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,JobTitle,JobCode,JobSummary,SkillQualification")] Job job, string[] selectedRequirments, string[] selectedSkills, string[] selectedQualifications)
        {
            try
            {
                //Add the selected requirement
                if (selectedRequirments != null)
                {
                    job.Requirements = new List<Requirement>();
                    foreach (var requirement in selectedRequirments)
                                                
                    {
                        var requirementToAdd = db.Requirements.Find(int.Parse(requirement));
                        job.Requirements.Add(requirementToAdd);
                    }
                }

                //Add the selected skill
                if (selectedSkills != null)
                {
                    job.Skills = new List<Skill>();
                    foreach (var skill in selectedSkills)
                    {
                        var skillToAdd = db.Skills.Find(int.Parse(skill));
                        job.Skills.Add(skillToAdd);
                    }
                }

                //Add the selected qualification
                if (selectedQualifications != null)
                {
                    job.Qualifications = new List<Qualification>();
                    foreach (var qualification in selectedQualifications)
                    {
                        var qualificationToAdd = db.Qualifications.Find(int.Parse(qualification));
                        job.Qualifications.Add(qualificationToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    db.Jobs.Add(job);
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

            PopulateAssignedRequirmentData(job);
            PopulateAssignedSkillData(job);
            PopulateAssignedQualificationData(job);

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            PopulateDropDownLists(job);

            PopulateAssignedSkillData(job);
            PopulateAssignedQualificationData(job);
            PopulateAssignedRequirmentData(job);

            return View(job);
        }

        // POST: Postings/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedSkills, string[] selectedQuals, string[] selectedReqs)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobToUpdate = db.Jobs
                .Where(p => p.ID == id).SingleOrDefault();

            if (TryUpdateModel(jobToUpdate, "",
               new string[] { "JobTitle", "JobSummary" }))
            {
                try
                {

                    UpdateJobQualifications(selectedQuals, jobToUpdate);
                    UpdateJobRequirements(selectedReqs, jobToUpdate);
                    UpdateJobSkills(selectedSkills, jobToUpdate);

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

            PopulateDropDownLists(jobToUpdate);

            PopulateAssignedSkillData(jobToUpdate);
            PopulateAssignedQualificationData(jobToUpdate);
            PopulateAssignedRequirmentData(jobToUpdate);

            return View(jobToUpdate);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        
        //For protection against hacker!!! 
        // POST: Postings/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            try
            {
                db.Jobs.Remove(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "You cannot delete this job if you made a posting with this.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(job);

        }

        /////// for update
        private void UpdateJobSkills(string[] selectedSkills, Job jobToUpdate)
        {
            if (selectedSkills == null)
            {
                jobToUpdate.Skills = new List<Skill>();
                return;
            }

            var selectedSkillsHS = new HashSet<string>(selectedSkills);
            var jobSkills = new HashSet<int>
                (jobToUpdate.Skills.Select(c => c.ID));//IDs of the currently selected skills
            foreach (var skill in db.Skills)
            {
                if (selectedSkillsHS.Contains(skill.ID.ToString()))
                {
                    if (!jobSkills.Contains(skill.ID))
                    {
                        jobToUpdate.Skills.Add(skill);
                    }
                }
                else
                {
                    if (jobSkills.Contains(skill.ID))
                    {
                        jobToUpdate.Skills.Remove(skill);
                    }
                }
            }
        }

        private void UpdateJobRequirements(string[] selectedReqs, Job jobToUpdate)
        {
            if (selectedReqs == null)
            {
                jobToUpdate.Requirements = new List<Requirement>();
                return;
            }

            var selectedRequirementHS = new HashSet<string>(selectedReqs);
            var jobRequirements = new HashSet<int>
                (jobToUpdate.Requirements.Select(c => c.ID));//IDs of the currently selected Reqs
            foreach (var reqs in db.Requirements)
            {
                if (selectedRequirementHS.Contains(reqs.ID.ToString()))
                {
                    if (!jobRequirements.Contains(reqs.ID))
                    {
                        jobToUpdate.Requirements.Add(reqs);
                    }
                }
                else
                {
                    if (jobRequirements.Contains(reqs.ID))
                    {
                        jobToUpdate.Requirements.Remove(reqs);
                    }
                }
            }
        }

        private void UpdateJobQualifications(string[] selectedQuals, Job jobToUpdate)
        {
            if (selectedQuals == null)
            {
                jobToUpdate.Qualifications = new List<Qualification>();
                return;
            }

            var selectedQualificationHS = new HashSet<string>(selectedQuals);
            var jobQualification = new HashSet<int>
                (jobToUpdate.Qualifications.Select(c => c.ID));//IDs of the currently selected Qualification
            foreach (var Qual in db.Qualifications)
            {
                if (selectedQualificationHS.Contains(Qual.ID.ToString()))
                {
                    if (!jobQualification.Contains(Qual.ID))
                    {
                        jobToUpdate.Qualifications.Add(Qual);
                    }
                }
                else
                {
                    if (jobQualification.Contains(Qual.ID))
                    {
                        jobToUpdate.Qualifications.Remove(Qual);
                    }
                }
            }
        }

        ////// Populate

        private void PopulateAssignedQualificationData(Job job)
        {
            var allQualifications = db.Qualifications;
            var appQualifications = new HashSet<int>(job.Qualifications.Select(b => b.ID));
            var viewModel = new List<AssignedQualificationVM>();
            foreach (var sq in allQualifications)
            {
                viewModel.Add(new AssignedQualificationVM
                {
                    QualificationID = sq.ID,
                    QualificationSet = sq.QualificationSet,
                    Assigned = appQualifications.Contains(sq.ID)
                });
            }
            ViewBag.Qualifications = viewModel;
        }

        private void PopulateAssignedSkillData(Job job)
        {
            var allSkills = db.Skills;
            var appSkills = new HashSet<int>(job.Skills.Select(b => b.ID));
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
                     
        private void PopulateAssignedRequirmentData(Job job)
        {
            var allRequirments = db.Requirements;
            var appRequirments = new HashSet<int>(job.Requirements.Select(b => b.ID));
            var viewModel = new List<AssignedRequirmentVM>();
            foreach (var sr in allRequirments)
            {
                viewModel.Add(new AssignedRequirmentVM
                {
                    RequirmentID = sr.ID,
                    RequirementName = sr.RequirementName,
                    Assigned = appRequirments.Contains(sr.ID)
                });
            }
            ViewBag.Requirements = viewModel;
        }

        private void PopulateDropDownLists(Job job = null)
        {
            
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
