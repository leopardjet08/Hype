using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class Requirement
    {
        public Requirement()
        {
            this.Qualifications = new HashSet<Qualification>();
        }

        public int ID  { get; set; }

        public int JobID { get; set; }

        public ICollection<Qualification> Qualifications { get; set; }
        public virtual Job Jobs { get; set; }
    }
}