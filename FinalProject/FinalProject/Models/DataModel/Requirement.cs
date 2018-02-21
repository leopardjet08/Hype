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
        public int ID  { get; set; }

        [Display(Name = "Skill")]
        [Required(ErrorMessage = "You cannot leave the name of the requirement blank.")]
        [StringLength(255, ErrorMessage = "Name too long.")]
        [Index("IX_Unique_Requirement", IsUnique = true)]
        public string RequirementName { get; set; }

        // generate a new table to store IDs for Many to many relationship
        public virtual ICollection<Job> Jobs { get; set; }
    }
}