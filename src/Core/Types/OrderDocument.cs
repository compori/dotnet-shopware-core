using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class OrderDocument
    {
        [JsonProperty(PropertyName = "documentId")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "documentDeepLink")]
        public string DocumentDeepLink { get; set; }
    }
}
