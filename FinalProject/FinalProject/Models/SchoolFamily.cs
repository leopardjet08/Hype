using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class SchoolFamily
    {
        public SchoolFamily()
        {
            this.Schools = new HashSet<School>();
        }

        public int ID { get; set; }

        [Display(Name = "School Family")]
        [Required(ErrorMessage = "Please input school family name.")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        [Index("IX_Unique_FamilyName", IsUnique = true)]
        public string FamilyName { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}