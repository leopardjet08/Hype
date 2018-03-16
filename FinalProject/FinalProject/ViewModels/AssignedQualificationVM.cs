using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class AssignedQualificationVM
    {
        public int QualificationID { get; set; }
        public string QualificationSet { get; set; }
        public bool Assigned { get; set; }
    }
}