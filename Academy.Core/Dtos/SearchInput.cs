using System;

namespace Academy.Core.Dtos
{
    public class SearchInput
    {
        public int GroupNumber { get; set; }
        public int BatchNumber { get; set; }
        public bool Active { get; set; }
        public bool IsBlackList { get; set; }
    }
}
