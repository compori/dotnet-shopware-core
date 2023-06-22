using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware media folder entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/1b24353e992b7-media-folder">Media Folder Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "media_folder", api: "media-folder")]
    public class MediaFolder : Entity
    {
        [JsonProperty(PropertyName = "useParentConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UseParentConfiguration { get; set; }        

        [JsonProperty(PropertyName = "configurationId", Required = Required.Always)]
        public string ConfigurationId { get; set; }

        [JsonProperty(PropertyName = "defaultFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultFolderId { get; set; }

        [JsonProperty(PropertyName = "parentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }

        [JsonProperty(PropertyName = "childCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? ChildCount { get; set; }

        [JsonProperty(PropertyName = "path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }
    }
}
