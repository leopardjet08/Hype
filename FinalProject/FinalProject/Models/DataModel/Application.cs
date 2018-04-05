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

        public int PostingID { get; set; }


        public int ApplicantID { get; set; }

        public Application()
        {

            SubmissionDate = DateTime.Now;

        }
        public DateTime SubmissionDate { get; private set; }

        // Value 1 default ID for Pending status
        public int ApplicationStatusID { get; set; } = 1;

        public virtual Applicant Applicant { get; set; }
        public virtual Posting Posting { get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }

    }
}
