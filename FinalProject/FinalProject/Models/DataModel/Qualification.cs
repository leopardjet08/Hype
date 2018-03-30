using FinalProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Qualification
    {
        public Qualification()
        {
            this.Jobs = new HashSet<Job>();
        }

        public int ID { get; set; }

        [Display(Name = "Qualifications")]
        [Required(ErrorMessage = "You cannot leave the name of the qualification blank.")]
        [StringLength(255, ErrorMessage = "Qualifications cannot exceed 255 characters.")]
        public string QualificationSet { get; set; }
        
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}