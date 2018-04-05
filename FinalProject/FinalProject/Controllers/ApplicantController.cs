using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Models.DataModel;
using PagedList;
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

namespace FinalProject.Controllers
{
    public class ApplicantController : Controller
    {
        private JobPostingCFEntities db = new JobPostingCFEntities();
        // GET: Applicant
        public ActionResult Index(string sortDirection, string sortField,
            string actionButton, string searchName, int? page)

        {
            ViewBag.Filtering = "";

            var applicants = from s in db.Applicants  select s;
            IQueryable app = db.Applicants.Include(s => s.Files).Include(a => a.ApplicantImage).Include(a => a.City).Include(a => a.Province);


            //Search bar code

            if (!String.IsNullOrEmpty(searchName))
            {
                applicants = applicants.Where(p => p.FName.ToUpper().Contains(searchName.ToUpper()) || p.LName.ToUpper().Contains(searchName.ToUpper()) );
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
                        .OrderBy(p => p.LName);
                }
                else
                {
                    applicants = applicants
                        .OrderByDescending(p => p.LName);
                }
            }
            
            else //By default sort by Name 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applicants = applicants
                        .OrderBy(p => p.FName);
                }
                else
                {
                    applicants = applicants
                        .OrderByDescending(p => p.FName);
                }
            }

            //Set sort for next time
            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            //number of data in the table
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.num = app;
            return View( applicants.ToPagedList(pageNumber, pageSize));
        }
        //details controller
        public ActionResult Details(int? id, string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get all posting data
            Applicant applicant = db.Applicants
                .Include(s =>s.Files)
                .Include(a => a.ApplicantImage)
                .Where(p => p.ID == id).SingleOrDefault();
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
                new string[] { "FName", "MName", "LName", "PhoneNumber", "EMail", "Address", "ProvinceID", "CityID" }))
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


        public ActionResult Archive(int? id)
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

        //For protection against hacker!!! 
        // POST: Postings/Delete
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            try
            {
                db.Applicants.Remove(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "You cannot delete a Applicant that has applications in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(applicant);

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

        private void PopulateDropDownLists(Applicant applicant = null)
        {
            ViewBag.ID = new SelectList(db.ApplicantImages, "ApplicantImageID", "ImageMimeType", applicant.ID);
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", applicant.CityID);
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ID", "ProvinceName", applicant.ProvinceID);
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

       

    }
}