using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class School
    {
        public School()
        {
            this.Postings = new HashSet<Posting>();
            this.Applications = new HashSet<Application>();
            this.SchoolLocations = new HashSet<SchoolLocation>();
        }

        public int ID { get; set; }

        [Display(Name = "School Name")]
        [Required(ErrorMessage = "Please provide school name.")]
        [StringLength(80, ErrorMessage = "School cannot exceed 80 characters.")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Please specify the school level.")]
        public int SchoolLevelID { get; set; }

        // For elementary level schools
        public int SchoolFamilyID { get; set; }

        public virtual SchoolLevel SchoolLevel { get; set; }
        public virtual SchoolFamily SchoolFamily { get; set; }
        public ICollection<Posting> Postings { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<SchoolLocation> SchoolLocations { get; set; }
    }
}