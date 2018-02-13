using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class ApplicationStatus
    {
        public ApplicationStatus()
        {
            this.Applications = new HashSet<Application>();
        }

        public int ID { get; set; }

        public string Status { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}