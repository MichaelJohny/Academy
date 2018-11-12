using System.Collections.Generic;
using Academy.Core.Batchs;

namespace Academy.Core.ViewModels
{
    public class BatchViewModel
    {
        public IEnumerable<Batch> Batches { get; set; }
        public string SearchTerm { get; set; }
    }
}
