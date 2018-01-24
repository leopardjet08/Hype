using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    // Pass City location of the school
    public class SchoolLocation
    {
        public SchoolLocation()
        {
            this.Schools = new HashSet<School>();
        }

        public int ID { get; set; }

        [Display(Name = "School Location")]
        [Required(ErrorMessage = "Please specify the location.")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        [Index("IX_Unique_LocationName", IsUnique = true)]
        public string LocationName { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}