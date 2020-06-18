using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServices.Notifier.Data
{
    public class ProductChangesDto
    {
        public int Number { get; set; }
        public int Qty { get; set; }
        public int State { get; set; }
        public int PrevQty { get; set; }
        public int PrevState { get; set; }
    }
}
