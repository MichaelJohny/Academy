using Academy.Core.ComplexTypes;

namespace Academy.Core.Courses
{
    public class CourseLabs :BaseComplex
    {
        public int CourseLocationId { get; set; }
        public virtual CourseLocation CourseLocation { get; set; }
    }
}
