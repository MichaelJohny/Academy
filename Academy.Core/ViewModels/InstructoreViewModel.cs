using System.Collections.Generic;
using Academy.Core.Instructors;

namespace Academy.Core.ViewModels
{
    public class InstructoreViewModel
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public string SearchTerm { get; set; }

    }
}
