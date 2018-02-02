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
        public int ID { get; set; }

        [Display(Name = "Qualifications")]
        [Required(ErrorMessage = "You cannot leave the name of the qualification blank.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Index("IX_Unique_Skill", IsUnique = true)]
        public string QualificationName { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}