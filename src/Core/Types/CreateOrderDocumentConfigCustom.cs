using Newtonsoft.Json;
using System;

namespace Compori.Shopware.Types
{
    public class CreateOrderDocumentConfigCustom
    {
        [JsonProperty(PropertyName = "stornoNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string StornoNumber { get; set; }

        [JsonProperty(PropertyName = "invoiceNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumber { get; set; }

        [JsonProperty(PropertyName = "deliveryNoteNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryNoteNumber { get; set; }

        [JsonProperty(PropertyName = "deliveryNoteDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeliveryNoteDate { get; set; }

        [JsonProperty(PropertyName = "deliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeliveryDate { get; set; }
    }
}
