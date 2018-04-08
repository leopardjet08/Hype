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

        public int ApplicationID { get; set; }

        public int ApplicantID { get; set; }

        public DateTime SubmissionDate { get;  set; }

        public DateTime ArchiveDate { get; set; }

        public virtual Application Applications { get; set; }
    }
}