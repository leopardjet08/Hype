using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;

namespace FinalProject.Controllers
{
    public class ApplicantsController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();

        // GET: Applicants
        public ActionResult Index()
        {
            IQueryable applicants = db.Applicants.Include(s=>s.Files).Include(a => a.ApplicantImage).Include(a => a.City).Include(a => a.Province);

            return View(applicants);
        }

        // GET: Applicants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        public FileContentResult Download(int id)
        {
            var theFile = db.Files.Include(f => f.FileContent).Where(f => f.ID == id).SingleOrDefault();
            return File(theFile.FileContent.Content, theFile.FileContent.MimeType, theFile.fileName);
        }

        // GET: Applicants/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.ApplicantImages, "ApplicantImageID", "ImageMimeType");
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName");
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ID", "ProvinceName");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FName,MName,LName,EMail,PhoneNumber,Address,ProvinceID,CityID,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,RowVersion")] Applicant applicant, IEnumerable<HttpPostedFileBase> theFiles)
        {
            if (ModelState.IsValid)
            {
                AddPicture(ref applicant, Request.Files["thePicture"]);
                AddDocuments(ref applicant, theFiles);
                db.Applicants.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDownLists(applicant);
            return View(applicant);
        }
        private void AddPicture(ref Applicant applicant, HttpPostedFileBase f)
        {
            if (f != null)
            {
                string mimeType = f.ContentType;
                int fileLength = f.ContentLength;
                if ((mimeType.Contains("image") && fileLength > 0))//Looks like we have a file!!!
                {
                    //Save the full size image
                    Stream fileStream = f.InputStream;
                    byte[] fullData = new byte[fileLength];
                    fileStream.Read(fullData, 0, fileLength);
                    //This is used for both Create and Edit so must decide
                    if (applicant.ApplicantImage == null)//Create New 
                    {
                        ApplicantImage fullImage = new ApplicantImage
                        {
                            ImageContent = fullData,
                            ImageMimeType = mimeType
                        };
                        applicant.ApplicantImage = fullImage;
                    }
                    else //Update the current image
                    {
                        applicant.ApplicantImage.ImageContent = fullData;
                        applicant.ApplicantImage.ImageMimeType = mimeType;
                    }
                }
            }

        }

        private void AddDocuments(ref Applicant applicant, IEnumerable<HttpPostedFileBase> docs)
        {
            foreach (var f in docs)
            {
                if (f != null)
                {
                    string mimeType = f.ContentType;
                    string fileName = Path.GetFileName(f.FileName);
                    int fileLength = f.ContentLength;
                    //Note: you could filter for mime types if you only want to allow
                    //certain types of files.  I am allowing everything.
                    if (!(fileName == "" || fileLength == 0))//Looks like we have a file!!!
                    {
                        Stream fileStream = f.InputStream;
                        byte[] fileData = new byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);

                        aFile newFile = new aFile
                        {
                            FileContent = new FileContent
                            {
                                Content = fileData,
                                MimeType = mimeType
                            },
                            fileName = fileName
                        };
                        applicant.Files.Add(newFile);
                    };
                }
            }
        }

        // GET: Applicants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants
                .Include(a => a.Files)
                .Include(a => a.ApplicantImage)
                .Where(a => a.ID == id).SingleOrDefault();
            if (applicant == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownLists(applicant);
            return View(applicant);
        }


        // POST: Applicants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, Byte[] rowVersion, string chkRemoveImage, IEnumerable<HttpPostedFileBase> theFiles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicantToUpdate = db.Applicants
                .Include(a => a.Files)
                .Include(a => a.ApplicantImage)
                .Where(a => a.ID == id).SingleOrDefault();

            if (TryUpdateModel(applicantToUpdate, "",
                new string[] { "FName", "MName", "LName", "PhoneNumber", "EMail","Address","ProvinceID","CityID" }))
            {
                try
                {
                    if (chkRemoveImage != null)
                    {
                        applicantToUpdate.ApplicantImage = null;
                    }
                    else
                    {
                        AddPicture(ref applicantToUpdate, Request.Files["thePicture"]);
                    }
                    AddDocuments(ref applicantToUpdate, theFiles);

                    //db.Entry(applicantToUpdate).OriginalValues["RowVersion"] = rowVersion;
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
                    var clientValues = (Applicant)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Applicant was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Applicant)databaseEntry.ToObject();
                        if (databaseValues.FName != clientValues.FName)
                            ModelState.AddModelError("FName", "Current value: "
                                + databaseValues.FName);
                        if (databaseValues.MName != clientValues.MName)
                            ModelState.AddModelError("MName", "Current value: "
                                + databaseValues.MName);
                        if (databaseValues.LName != clientValues.LName)
                            ModelState.AddModelError("LName", "Current value: "
                                + databaseValues.LName);
                        if (databaseValues.PhoneNumber != clientValues.PhoneNumber)
                            ModelState.AddModelError("PhoneNumber", "Current value: "
                                + String.Format("{0:(###) ###-####}", databaseValues.PhoneNumber));
                        if (databaseValues.EMail != clientValues.EMail)
                            ModelState.AddModelError("EMail", "Current value: "
                                + databaseValues.EMail);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        applicantToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (DataException dex)
                {
                    if (dex.InnerException.InnerException.Message.Contains("IX_Unique"))
                    {
                        ModelState.AddModelError("EMail", "Unable to save changes. Remember, you cannot have duplicate eMail address.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists go for coffee.");
                    }
                }

            }
            
            PopulateDropDownLists(applicantToUpdate);
            return View(applicantToUpdate);
        }

        private void PopulateDropDownLists(Applicant applicant = null)
        {
            ViewBag.ID = new SelectList(db.ApplicantImages, "ApplicantImageID", "ImageMimeType", applicant.ID);
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", applicant.CityID);
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ID", "ProvinceName", applicant.ProvinceID);
        }

        // GET: Applicants/Delete/5
        public ActionResult Delete(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            db.Applicants.Remove(applicant);
            db.SaveChanges();
            return RedirectToAction("Index");
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
