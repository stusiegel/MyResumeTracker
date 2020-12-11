using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Job
    {

        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "Job Title cannot exceed 50 characters")]
        [Display(Name = "Job TItle")]
        public string Title { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }
        
        public string Comments { get; set; }
        //public string SourceURL { get; set; }
        
       // public bool SubmittedApplication { get; set; }
       // public DateTime DatePosted { get; set; }
       // public DateTime DateSubmitted { get; set; }
        
        public decimal Salary { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Zipcode { get; set; }

        public string CompanyName { get; set; }

        [Required]
        public JobRatingEnum? Rating { get; set; }
    }
}
