using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Posting
    {
        public Posting()
        {
            this.Applications = new HashSet<Application>();
        }

        public int ID { get; set; }

        [Display(Name = "Number of Openings")]
        [Required(ErrorMessage = "You must specify how many positions are open.")]
        [Range(1, 9999, ErrorMessage = "Invalid number of job openings.")]
        public int NumberOpen { get; set; }

        [Display(Name = "Closing Date")]
        [Required(ErrorMessage = "You must specify the closing date for the job posting.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please Select the school for this job posting.")]
        public int SchoolID { get; set; }

        public virtual School Schools { get; set; }
        public ICollection<Application> Applications { get; set; }



        // Validation for date
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ClosingDate < DateTime.Today)
            {
                yield return new ValidationResult("The closing date cannot be in the past.", new[] { "ClosingDate" });
            }
            if (StartDate.GetValueOrDefault() < ClosingDate)
            {
                yield return new ValidationResult("The start date for the posting cannot be before the closing date.", new[] { "StartDate" });
            }
        }

    }
}