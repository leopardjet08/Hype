using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class Skill
    {
        public Skill()
        {
            this.Jobs = new HashSet<Job>();
        }

        public int ID { get; set; }

        [Display(Name = "Skills")]
        [Required(ErrorMessage = "You cannot leave the name of the qualification blank.")]
        [StringLength(255, ErrorMessage = "Skills cannot exceed 255 characters.")]
        public string SkillName { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}
