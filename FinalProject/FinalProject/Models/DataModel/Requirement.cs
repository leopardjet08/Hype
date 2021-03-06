﻿using System;
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
        [Index("IX_Unique_Req", IsUnique = true)]
        [Display(Name = "Requirement")]
        [Required(ErrorMessage = "Provide Requirement Description.")]
        [StringLength(50, ErrorMessage = "Name too long.")]
        public string RequirementName { get; set; }
        
        
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}