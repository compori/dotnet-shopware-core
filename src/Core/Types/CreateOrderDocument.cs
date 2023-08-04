using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class CreateOrderDocument
    {
        [JsonProperty(PropertyName = "config")]
        public CreateOrderDocumentConfig Config { get; set; }

        [JsonProperty(PropertyName = "referenced_document_id", NullValueHandling = NullValueHandling.Include)]
        public string ReferencedDocumentId { get; set; }

        [JsonProperty(PropertyName = "static")]
        public bool Static { get; set; }
    }
}
