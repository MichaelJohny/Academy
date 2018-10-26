using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Academy.Core.Base;
using Academy.Core.ComplexTypes;
using Academy.Core.Enrollments;
using Academy.Core.Enums;

namespace Academy.Core.Students
{
    public class Student : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Column("FirstName")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Column("SecondName")]
        [StringLength(50, ErrorMessage = "Second name cannot be longer than 50 characters.")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required, Column("ThirdName")]
        [StringLength(50, ErrorMessage = "Third name cannot be longer than 50 characters.")]
        [Display(Name = "Third Name")]
        public string ThirdName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {SecondName} {ThirdName}";


        [Required, Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Display(Name = "BlackList")]
        public bool IsBlackList { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string Mobile1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile2 { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Display(Name = "Address Description")]
        public string AddressDescrption { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string Collage { get; set; }

        public string Specialization { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Graduation Year")]
        public string GraduationYear { get; set; }

        public string Experience { get; set; }

        public StudentStatus Status { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public IEnumerable<NationalityComplex> GetNationalityList()
        {
            return new HashSet<NationalityComplex>()
            {
                new NationalityComplex() {Id = 1, Name = "Egyption"},
                new NationalityComplex() {Id = 1, Name = "American"},
                new NationalityComplex() {Id = 1, Name = "Australian"},
                new NationalityComplex() {Id = 1, Name = "Candian"}
            }; 
        }
        public IEnumerable<CollageComplex> GetQualificationList()
        {
            return new HashSet<CollageComplex>()
            {
                new CollageComplex() {Id = 1, Name = "Computer Science"},
                new CollageComplex() {Id = 1, Name = "Commerce"},
                new CollageComplex() {Id = 1, Name = "Accountant"},
                new CollageComplex() {Id = 1, Name = "Medicin"}
            };
        }
        public IEnumerable<QualificationComplex> GetCollageList()
        {
            return new HashSet<QualificationComplex>()
            {
                new QualificationComplex() {Id = 1, Name = "Master"},
                new QualificationComplex() {Id = 1, Name = "BCs"},
            };
        }

    }
}
