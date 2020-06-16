using System.Collections.Generic;

namespace ProductServices.Notifier.Data
{
    public class ProductChangesFilterDto
    {
        public IEnumerable<string> QtyCh { get; set; }
        public IEnumerable<string> StateCh { get; set; }
        public IEnumerable<int> Categories { get; set; }
    }
}