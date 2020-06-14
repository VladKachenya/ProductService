using System;
using System.Collections.Generic;
using System.Linq;
using ProductService.DataTransfer.Data;

namespace ProductService.DataTransfer.Client.Factories
{
    public class RoutingKeyFactory
    {
        public string GetPublicationKey(ProductChange productChange)
        {
            return FormRouteKey(productChange.GetQtyChangeType().ToString(), 
                productChange.GetStateChangeType().ToString(), 
                productChange.Number.ToString());
        }

        public IEnumerable<string>GetBindingKeys(ProductChangesFilter productChangesFilter)
        {
            var qtyKeys = GetKeys(productChangesFilter.QtyChanges);
            var stateKeys = GetKeys(productChangesFilter.StateChanges);
            var productNumbersKeys = productChangesFilter.ProductNumbers.Count != 0 ? 
                productChangesFilter.ProductNumbers.Select(i => i.ToString()).ToList() : 
                new List<string>{"*"};

            var res = new List<string>();
            foreach (var qtyKey in qtyKeys)
            {
                foreach (var stateKey in stateKeys)
                {
                    foreach (var productNumbersKey in productNumbersKeys)
                    {
                        res.Add(FormRouteKey(qtyKey, stateKey, productNumbersKey));
                    }
                }
            }

            return res;
        }

        private string FormRouteKey(string qty, string state, string number)
        {
            return $"{qty}.{state}.{number}";
        }

        //
        private List<string> GetKeys(ChangeType changeType)
        {
            var res = new List<string>();
            var changeTypes = new List<ChangeType>();
            foreach (var type in Enum.GetValues(typeof(ChangeType)))
            {
                changeTypes.Add((ChangeType)type);
            }

            if(changeTypes.All(i => (i & changeType) != 0) || changeType == default(ChangeType))
            {
                res.Add("*");
                return res;
            }

            foreach (var type in changeTypes)
            {
                if ((type & changeType) != 0)
                {
                    res.Add(type.ToString());
                }
            }

            return res;
        }
    }
}