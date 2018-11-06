using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Core.Courses;
using Academy.Core.Students;

namespace Academy.Core.ViewModels
{
    public class StudentCourseViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
