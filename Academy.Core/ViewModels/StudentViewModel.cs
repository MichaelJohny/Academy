using System.Collections.Generic;
using Academy.Core.Students;

namespace Academy.Core.ViewModels
{
    public class StudentViewModel
    {
        public  IEnumerable<Student> Students { get; set; }
        public string SearchTerm { get; set; }
    }
}
