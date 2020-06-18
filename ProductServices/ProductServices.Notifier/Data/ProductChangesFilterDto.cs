using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProductServices.Notifier.Data
{
    public class ProductChangesFilterDto
    {
        public List<string> QtyCh { get; set; }

        public List<string> StateCh { get; set; }

        public List<int> Categories { get; set; }
    }
}