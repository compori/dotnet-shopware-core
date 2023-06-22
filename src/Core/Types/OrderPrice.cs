using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class OrderPrice
    {
        [JsonProperty(PropertyName = "totalPrice")]
        public double TotalPrice { get; set; }

        [JsonProperty(PropertyName = "netPrice")]
        public double NetPrice { get; set; }

        [JsonProperty(PropertyName = "positionPrice")]
        public double PositionPrice { get; set; }

        [JsonProperty(PropertyName = "taxStatus")]
        public string TaxStatus { get; set; }

        [JsonProperty(PropertyName = "rawTotal")]
        public double RawTotal { get; set; }

        [JsonProperty(PropertyName = "calculatedTaxes")]
        public List<CalculatedTax> CalculatedTaxes { get; set; }
    }
}
