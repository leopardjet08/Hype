using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    // List of provinces for DropDownList
    public class Province
    {
        public Province()
        {
            this.Applicants = new HashSet<Applicant>();
        }
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        [Required(ErrorMessage = "Provide Province Name")]
        public string ProvinceName { get; set; }

        public ICollection<Applicant> Applicants { get; set; }
    }
}