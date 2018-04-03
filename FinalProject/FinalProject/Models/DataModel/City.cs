using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models.DataModel
{
    public class City
    {
        //public City()
        //{
        //    this.Applicants = new HashSet<Applicant>();
        //    this.Schools = new HashSet<School>();
        //}

        public int ID { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City name required.")]
        [StringLength(50, ErrorMessage = "Character Length Exceeded.")]
        //[Index("IX_Unique_City", IsUnique = true)]
        public string CityName { get; set; }

        public ICollection<Applicant> Applicants { get; set; }
        public ICollection<School> Schools { get; set; }

    }
}