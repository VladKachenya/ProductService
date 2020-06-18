using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.DataTransfer.Data;

namespace ProductServices.Notifier.Data
{
    public class DataMapper
    {
        public ProductChangesFilter ToProductChangesFilter(ProductChangesFilterDto changesFilterDto)
        {
            var res = new ProductChangesFilter();
            changesFilterDto.Categories ??= new List<int>();
            changesFilterDto.QtyCh ??= new List<string>();
            changesFilterDto.StateCh ??= new List<string>();


            foreach (var category in changesFilterDto.Categories)
            {
                if (!res.ProductCategories.Contains(category))
                {
                    res.ProductCategories.Add(category);
                }
            }

            res.QtyChanges = ToChangeType(changesFilterDto.QtyCh);
            res.StateChanges = ToChangeType(changesFilterDto.StateCh);

            return res;
        }

        public ProductChangesDto ToProductChangesDto(ProductChanges productChanges)
        {

            return new ProductChangesDto
            {
                Qty = productChanges.Qty,
                State = productChanges.State,
                Number = productChanges.Number,
                PrevQty = productChanges.PrevQty,
                PrevState = productChanges.PrevState
            };
        }

        private ChangeType ToChangeType(IEnumerable<string> changes)
        {
            var res = default(ChangeType);
            foreach (var change in changes)
            {
                Enum.TryParse(change, out ChangeType parseRes);
                res |= parseRes;
            }

            return res;
        }
    }
}
