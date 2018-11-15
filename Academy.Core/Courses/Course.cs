using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Academy.Core.Base;
using Academy.Core.Batchs;
using Academy.Core.DropLists;
using Academy.Core.Enrollments;
using Academy.Core.Instructors;

namespace Academy.Core.Courses
{
    public class Course : BaseEntity
    {
        [Display(Name = "Course Name")]
        public int CourseNameId { get; set; }

        //[Display(Name = "Course Category")]
        //public int CategoryId { get; set; }

        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }

        [Display(Name = "Batch")]
        public int BatchId { get; set; }

        [Display(Name = "Course Manager")]
        public int CourseManagerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Time From")]
        public TimeSpan TimeFrom { get; set; }

        [Display(Name = "Time To")]
        public TimeSpan TimeTo { get; set; }

        [Display(Name = "Course Location")]
        public int CourseLocationId { get; set; }

        [Required, Display(Name = "Course Lab")]
        public int CourseLabId { get; set; }

        [Display(Name = "Course Duration")]
        public double CourseDuration { get; set; }

        [Display(Name = "Course Fees")]
        public double CourseFees { get; set; }

        public string Day1 { get; set; }

        public string Day2 { get; set; }

        [Display(Name = "Group Number")]
        public int GroupNumber { get; set; }

        [ForeignKey("CourseNameId")]
        public virtual CourseNames CourseName { get; set; }

        //[ForeignKey("CategoryId")]
        //public virtual Category Category { get; set; }

        [ForeignKey("CourseLocationId")]
        public virtual CourseLocation CourseLocation { get; set; }

        [ForeignKey("CourseLabId")]
        public virtual CourseLabs CourseLab { get; set; }

        [ForeignKey("InstructorId")]
        public virtual Instructor Instructor { get; set; }

        [ForeignKey("BatchId")]
        public virtual Batch Batch { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public IEnumerable<string> GetDays()
        {
            return new HashSet<string>()
            {
                "Sat",
                "Sun",
                "Mon",
                "Tus",
                "Wend",
                "Thurs",
                "Fri"
            };
        }
    }
}
