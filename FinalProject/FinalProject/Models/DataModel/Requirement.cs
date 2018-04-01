using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class Requirement
    {
        public Requirement()
        {
            this.Jobs = new HashSet<Job>();
        }

        public int ID  { get; set; }

        [Display(Name = "Requirement")]
        [Required(ErrorMessage = "Provide Requirement Description.")]
        [StringLength(50, ErrorMessage = "Name too long.")]
        [Index("IX_Unique_Requirement", IsUnique = true)]
        public string RequirementName { get; set; }
        
        
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}