using FinalProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Posting : Auditable, IValidatableObject
    {
        public Posting()
        {
            this.Applications = new HashSet<Application>();
            this.SavedPostings = new HashSet<SavedPosting>();
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

        [Display(Name = "Posting Description")]
        [StringLength(2000, ErrorMessage = "Description must be between 20 and 2000 characters", MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        public string PostingDescription { get; set; }

        [Required(ErrorMessage = "Please specify the school you applied for.")]
        public int SchoolID { get; set; }

        [Required(ErrorMessage ="Please provide the Job for this posting")]
        public int JobID { get; set; }

        public virtual School School { get; set; }
        public virtual Job Job { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<SavedPosting> SavedPostings { get; set; }
        
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