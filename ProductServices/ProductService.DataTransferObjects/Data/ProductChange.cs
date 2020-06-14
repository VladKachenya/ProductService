using System;

namespace ProductService.DataTransfer.Data
{
    public class ProductChange
    {
        public int Number { get; set; }
        public int Qty { get; set; }
        public int State { get; set; }
        public int PrevQty { get; set; }
        public int PrevState { get; set; }

        public ChangeType GetQtyChangeType()
        {
            return GetChange(PrevQty, Qty);
        }

        public ChangeType GetStateChangeType()
        {
            return GetChange(PrevState, State);
        }

        public override string ToString()
        {
            return $"Number:{Number}, Qty:{Qty}, State:{State}, PrevQty:{PrevQty}, PrevState:{PrevState}";
        }

        private ChangeType GetChange(int preview, int current)
        {
            return current > preview ? ChangeType.Inc : current < preview ? ChangeType.Dec : ChangeType.NoChange;
        }
    }

    [Flags]
    public enum ChangeType
    {
        NoChange = 1,
        Inc = 2,
        Dec = 4
    }
}