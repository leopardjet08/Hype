using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Models.DataModel;

namespace FinalProject.Models
{
    public class Applicant : Auditable
    {
        public Applicant()
        {
            this.Applications = new HashSet<Application>();
            this.SavedPostings = new HashSet<SavedPosting>();
            this.Files = new HashSet<aFile>();

        }

        // Get Full Name
        [Display(Name = "Applicant")]
        public string FullName
        {
            get
            {
                return FName
                    + (string.IsNullOrEmpty(MName) ? " " :
                        (" " + (char?)MName[0] + ". ").ToUpper())
                    + LName;
            }
        }

        // Get Formal Name
        public string FormalName
        {
            get
            {
                return LName + ", " + FName
                    + (string.IsNullOrEmpty(MName) ? "" :
                        (" " + (char?)MName[0] + ".").ToUpper());
            }
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
        public string EMail { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 PhoneNumber { get; set; }

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

        //Added to hold picture of applicant
        public virtual ApplicantImage ApplicantImage { get; set; }

        //Added to hold related files
        public ICollection<aFile> Files { get; set; }


    }
}