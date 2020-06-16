using System.Collections.Generic;

namespace ProductService.DataTransfer.Data
{
    public class ProductChangesFilter
    {
        public ProductChangesFilter()
        {
            ProductCategories = new List<int>();
        }
        public ChangeType QtyChanges { get; set; }
        public ChangeType StateChanges { get; set; }
        public List<int> ProductCategories { get; }
    }
}