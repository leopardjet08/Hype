using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class School
    {
        public School()
        {
            this.Postings = new HashSet<Posting>();
            this.Applications = new HashSet<Application>();
        }

        public int ID { get; set; }

        [Display(Name = "School Name")]
        [Required(ErrorMessage = "Please provide school name.")]
        [StringLength(80, ErrorMessage = "School cannot exceed 80 characters.")]
        public string SchoolName { get; set; }

        public ICollection<Posting> Postings { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}