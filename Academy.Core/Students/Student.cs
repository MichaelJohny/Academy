using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Academy.Core.Base;
using Academy.Core.DropLists;
using Academy.Core.Enrollments;
using Academy.Core.Enums;
using Academy.Core.Validations;

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

        
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {SecondName} {ThirdName}";


        [Required, Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required, Display(Name = "Nationality")]
        public int NationalityId { get; set; }

        [Display(Name = "BlackList")]
        public bool IsBlackList { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        [MobileValidation]
        [Display(Name = "Mobile 1")]
        public string Mobile1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Mobile2Validation]
        [Display(Name = "Mobile 2")]
        public string Mobile2 { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Display(Name = "Address Description")]
        public string AddressDescrption { get; set; }

        [Required, Display(Name = "Qualifcation")]
        public int QualificationId { get; set; }

        [Required,Display(Name = "Collage")]
        public int CollageId { get; set; }

        public string Specialization { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Graduation Year")]
        public string GraduationYear { get; set; }

        public string Experience { get; set; }

        public string Employer { get; set; }

        public StudentStatus Status { get; set; }

        [Required, Display(Name = "City")]
        public int CityId { get; set; }

        [Required, Display(Name = "Area")]
        public int AreaId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        [ForeignKey("AreaId")]
        public Area Area { get; set; }

        [ForeignKey("CollageId")]
        public virtual Collage Collage { get; set; }

        [ForeignKey("NationalityId")]
        public virtual Nationality Nationality { get; set; }

        [ForeignKey("QualificationId")]
        public virtual Qualifiation Qualifiation { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        
        //public IEnumerable<string> GetNationalityList()
        //{
        //    return new HashSet<string>()
        //    {
        //        "Egyption",
        //        "American",
        //        "Australian",
        //        "Candian"
        //    };
        //}

        //public IEnumerable<NationalityComplex> GetNationalityList()
        //{
        //    return new HashSet<NationalityComplex>()
        //    {
        //        new NationalityComplex() {Id = 1, Name = "Egyption"},
        //        new NationalityComplex() {Id = 1, Name = "American"},
        //        new NationalityComplex() {Id = 1, Name = "Australian"},
        //        new NationalityComplex() {Id = 1, Name = "Candian"}
        //    };
        //}
        //public IEnumerable<string> GetCollageList()
        //{
        //    return new HashSet<string>()
        //    {
        //        "Computer Science",
        //        "Commerce",
        //        "Accountant",
        //        "Medicin"
        //    };
        //}
        //public IEnumerable<string> GetQualificationList()
        //{
        //    return new HashSet<string>()
        //    {
        //        "Master",
        //        "BCs",
        //    };
        //}

    }
}
