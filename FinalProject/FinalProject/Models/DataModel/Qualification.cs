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
        [Required(ErrorMessage = "You cannot leave the qualification blank.")]
        [StringLength(255, ErrorMessage = "Qualifications cannot exceed 255 characters.")]
        [Index("IX_Unique_Qualification", IsUnique = true)]
        public string QualificationSet { get; set; }

        // generate a new table to store IDs for Many to many relationship
        public virtual ICollection<Job> Jobs { get; set; }
    }
}