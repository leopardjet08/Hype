using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class SavedPosting
    {
        public SavedPosting()
        {
            this.Postings = new HashSet<Posting>();
        }
        public int ID { get; set; }

        public int ApplicantID { get; set; }

        public virtual Applicant Applicant { get; set; }
        public ICollection<Posting> Postings { get; set; }
    }
}