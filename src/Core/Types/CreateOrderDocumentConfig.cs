using Newtonsoft.Json;
using System;

namespace Compori.Shopware.Types
{
    public class CreateOrderDocumentConfig
    {
        [JsonProperty(PropertyName = "documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "documentComment")]
        public string DocumentComment { get; set; }

        [JsonProperty(PropertyName = "documentDate")]
        public DateTime DocumentDate { get; set; }

        [JsonProperty(PropertyName = "custom")]
        public CreateOrderDocumentConfigCustom Custom { get; set; }
    }
}
