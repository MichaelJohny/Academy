using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Academy.Core.Base;
using Academy.Core.Courses;
using Academy.Core.DropLists;

namespace Academy.Core.Batchs
{
    public class Batch : BaseEntity
    {
        [Display(Name = "Batch Number")]
        public int BatchNumber { get; set; }

        [Display(Name = "Batch Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
