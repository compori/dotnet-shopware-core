using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    /// <summary>
    /// Class CreateOrderDocument.
    /// 
    /// <see cref="https://shopware.stoplight.io/docs/admin-api/branches/v6.5/a75a90c0b7e9b-create-documents-for-orders"/>
    /// </summary>
    public class CreateOrderDocument
    {
        /// <summary>
        /// Gets or sets the identifier of the order..
        /// </summary>
        /// <value>The Identifier of the order.</value>
        [JsonProperty(PropertyName = "orderId", Required = Required.Always)]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the type of the document to be generated.
        /// </summary>
        /// <value>The type of the document to be generated.</value>
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type of document file to be generated.
        /// </summary>
        /// <value>The type of document file to be generated.</value>
        [JsonProperty(PropertyName = "fileType")]
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the document should be static or not.
        /// </summary>
        /// <value><c>true</c> if document sould be static; otherwise, <c>false</c>.</value>
        [JsonProperty(PropertyName = "static")]
        public bool Static { get; set; }

        /// <summary>
        /// Gets or sets the referenced document identifier.
        /// </summary>
        /// <value>The referenced document identifier.</value>
        [JsonProperty(PropertyName = "referencedDocumentId", NullValueHandling = NullValueHandling.Include)]
        public string ReferencedDocumentId { get; set; }

        [JsonProperty(PropertyName = "config")]
        public CreateOrderDocumentConfig Config { get; set; }
    }
}
