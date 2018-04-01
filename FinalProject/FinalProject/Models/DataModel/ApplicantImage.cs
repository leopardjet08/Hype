using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class ApplicantImage
    {
        [Key, ForeignKey("Applicant")]
        public int ApplicantImageID { get; set; }
        [ScaffoldColumn(false)]
        public byte[] ImageContent { get; set; }

        [StringLength(256)]
        [ScaffoldColumn(false)]
        public string ImageMimeType { get; set; }

        public virtual Applicant Applicant { get; set; }
    }
}