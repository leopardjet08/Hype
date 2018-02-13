using FinalProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Job
    {
        public Job()
        {
            this.Postings = new HashSet<Posting>();
            this.Requirements = new HashSet<Requirement>();
            this.Qualifications = new HashSet<Qualification>();
        }

        public int ID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Job title Required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Summary")]
        [StringLength(2000, ErrorMessage = "Summary must be between 20 and 2000 characters", MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        public string JobSummary { get; set; }

        public ICollection<Posting> Postings { get; set; }
        public ICollection<Requirement> Requirements { get; set; }
        public ICollection<Qualification> Qualifications { get; set; }
    }
}