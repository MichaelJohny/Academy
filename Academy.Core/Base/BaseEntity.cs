using System;
using Academy.Core.DynamicFilters;

namespace Academy.Core.Base
{
    public class BaseEntity:ISoftDelete
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
        public int CreatorUserId { get; set; }
        public int? ModifiedUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
