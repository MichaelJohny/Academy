using System.ComponentModel.DataAnnotations;
using Academy.Core.Base;
using Academy.Core.Courses;
using Academy.Core.Students;

namespace Academy.Core.Enrollments
{
    public class Enrollment : BaseEntity
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
