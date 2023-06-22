using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Types
{
    public class OrderTransactionAmount
    {
        [JsonProperty(PropertyName = "quantity")]
        public long Quantity { get; set; }

        [JsonProperty(PropertyName = "totalPrice")]
        public double TotalPrice { get; set; }

        [JsonProperty(PropertyName = "unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty(PropertyName = "calculatedTaxes")]
        public List<CalculatedTax> CalculatedTaxes { get; set; }
    }
}
