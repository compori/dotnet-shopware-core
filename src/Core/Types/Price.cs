using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Price
    {
        [JsonProperty(PropertyName = "currencyId")]
        public string CurrencyId { get; set; }

        [JsonProperty(PropertyName = "gross")]
        public double Gross { get; set; }

        [JsonProperty(PropertyName = "net")]
        public double Net { get; set; }

        /// <summary>
        /// Gross and net prices are linked to each other. Shopware will calculated one from the other them.
        /// </summary>
        /// <value><c>true</c> if linked; otherwise, <c>false</c>.</value>
        [JsonProperty(PropertyName = "linked")]
        public bool Linked { get; set; }

        [JsonProperty(PropertyName = "listPrice", NullValueHandling = NullValueHandling.Include)]
        public Price ListPrice { get; set; }
    }
}
