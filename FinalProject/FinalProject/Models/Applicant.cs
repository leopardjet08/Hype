using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Applicant
    {
        public Applicant()
        {
            this.Applications = new HashSet<Application>();
        }

        public string ID { get; set; }

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

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required.")]
        [StringLength(50, ErrorMessage = "Character Length Exceeded.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select Province.")]
        public int ProvinceID { get; set; }

        public virtual Province Province { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}