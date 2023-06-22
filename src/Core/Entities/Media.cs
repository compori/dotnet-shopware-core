using Compori.Shopware.Attributes;
using Newtonsoft.Json;
using System;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware media entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/edfea8df3ab7d-media">Media Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "media", api: "media")]
    public class Media : Entity
    {
        [JsonProperty(PropertyName = "mediaFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaFolderId { get; set; }

        [JsonProperty(PropertyName = "mimeType", NullValueHandling = NullValueHandling.Ignore)]
        public string MimeType { get; set; }
        
        [JsonProperty(PropertyName = "fileExtension", NullValueHandling = NullValueHandling.Ignore)]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = "uploadedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UploadedAt { get; set; }
        
        [JsonProperty(PropertyName = "fileName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }
        
        [JsonProperty(PropertyName = "fileSize", NullValueHandling = NullValueHandling.Ignore)]
        public long? FileSize { get; set; }

        [JsonProperty(PropertyName = "alt", NullValueHandling = NullValueHandling.Ignore)]
        public string Alt { get; set; }
        
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "hasFile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasFile { get; set; }

        [JsonProperty(PropertyName = "private", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Private { get; set; }
    }
}
