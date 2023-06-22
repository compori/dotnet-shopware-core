using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class CalculatedTax
    {
        [JsonProperty(PropertyName = "tax")]
        public double Tax { get; set; }

        [JsonProperty(PropertyName = "taxRate")]
        public double Rate { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
    }
}
