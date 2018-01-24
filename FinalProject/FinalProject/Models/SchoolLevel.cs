using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class SchoolLevel
    {
        public SchoolLevel()
        {
            this.Schools = new HashSet<School>();
        }

        public int ID { get; set; }

        [Display(Name = "School Level")]
        [Required(ErrorMessage = "Please input school level name.")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        [Index("IX_Unique_LevelName", IsUnique = true)]
        public string LevelName { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}