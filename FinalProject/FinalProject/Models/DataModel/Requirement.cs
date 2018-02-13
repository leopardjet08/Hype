using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class Requirement
    {
        public int ID  { get; set; }

        public string RequirementName { get; set; }

        public int JobID { get; set; }
        
        public virtual Job Jobs { get; set; }
    }
}