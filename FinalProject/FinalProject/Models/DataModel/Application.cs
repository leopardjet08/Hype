using FinalProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Application
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please specify the job posting applied for.")]
        [Index("IX_Unique_Posting", Order = 1, IsUnique = true)]
        public int PostingID { get; set; }

        [Required(ErrorMessage = "Please specify the applicant applying for the job posting.")]
        [Index("IX_Unique_Application", Order = 2, IsUnique = true)]
        public int ApplicantID { get; set; }

        // Value 1 if approved, set Default value is 0
        public int Approved { get; set; } = 0;

        public int ApprovedAppsID { get; set; } 

        public virtual Applicant Applicant { get; set; }
        public virtual Posting Posting { get; set; }
        public virtual ApprovedApps ApprovedApps { get; set; }

    }
}
