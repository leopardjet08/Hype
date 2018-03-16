using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class AssignedRequirmentVM
    {
        public int RequirmentID { get; set; }
        public string RequirementName { get; set; }
        public bool Assigned { get; set; }
    }
}