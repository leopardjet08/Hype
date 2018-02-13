using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Models.DataModel;

namespace FinalProject.Models
{
    public class Applicant
    {
        public Applicant()
        {
            this.Applications = new HashSet<Application>();
            this.SavedPostings = new HashSet<SavedPosting>();
        }

        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string MName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "First Name Required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string LName { get; set; }

        [Required(ErrorMessage = "E-mail Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        [Index("IX_Unique_Applicant_email", IsUnique = true)]
        public string eMail { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required.")]
        [StringLength(80, ErrorMessage = "Character Length Exceeded.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select Province.")]
        public int ProvinceID { get; set; }

        [Required(ErrorMessage = "Please select City.")]
        public int CityID { get; set; }

        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<SavedPosting> SavedPostings { get; set; }
    }
}