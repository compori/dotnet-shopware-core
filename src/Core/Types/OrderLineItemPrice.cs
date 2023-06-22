using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class OrderLineItemPrice
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
