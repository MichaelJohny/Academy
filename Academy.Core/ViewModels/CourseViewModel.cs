using System.Collections.Generic;
using Academy.Core.Courses;

namespace Academy.Core.ViewModels
{
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public string SearchTerm { get; set; }
    }
}
