using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class SavedPosting
    { 
        public int ID { get; set; }

        public int ApplicantID { get; set; }

        public int PostingID { get; set; }

        public virtual Applicant Applicant { get; set; }
        public virtual Posting Postings { get; set; }
    }
}