﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Core.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
        public int CreatorUserId { get; set; }
        public int? ModifiedUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}