using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware document entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/2274d4d521f4d-document">Document Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "document", api: "document")]
    public class Document : Entity
    {
        [JsonProperty(PropertyName = "deepLinkCode", NullValueHandling = NullValueHandling.Ignore)]
        public string DeepLinkCode { get; set; }

        [JsonProperty(PropertyName = "documentTypeId", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentTypeId { get; set; }
        
        [JsonProperty(PropertyName = "documentMediaFileId", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentMediaFileId { get; set; }
            
        [JsonProperty(PropertyName = "fileType", NullValueHandling = NullValueHandling.Ignore)]
        public string FileType { get; set; }

        [JsonProperty(PropertyName = "orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }
        
        [JsonProperty(PropertyName = "orderVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderVersionId { get; set; }
    }
}
