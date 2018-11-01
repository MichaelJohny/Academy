using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Academy.Core.Base;
using Academy.Core.Courses;
using Academy.Core.Validations;

namespace Academy.Core.Instructors
{
    public class Instructor : BaseEntity
    {
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstMidName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName => $"{FirstMidName} {LastName}";

        [DataType(DataType.PhoneNumber)]
        [InstructorMobileValidation]
        [Display(Name = "Mobile 1")]
        public string Mobile1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [InstructorMobile2Validation]
        [Display(Name = "Mobile 2")]
        public string Mobile2 { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        [Required,Display(Name = "ID Number")]
        public string IdNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
