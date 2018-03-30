using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class ArchiveApplication
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please specify the job posting applied for.")]
        [Index("IX_Unique_ArchivePosting", Order = 1, IsUnique = true)]
        public int PostingID { get; set; }

        [Required(ErrorMessage = "Please specify the applicant applying for the job posting.")]
        [Index("IX_Unique_ArchiveApplication", Order = 2, IsUnique = true)]
        public int ApplicantID { get; set; }

        public DateTime SubmissionDate { get; private set; }

        public DateTime ArchiveDate { get;  set; }

        public int ApplicationStatusID { get; set; }

        public virtual Applicant Applicant { get; set; }
        public virtual Posting Posting { get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }
    }
}