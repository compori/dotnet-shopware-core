using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware media default folder entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/296cb386990df-media-default-folder">Media Default Folder Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "media_default_folder", api: "media-default-folder")]
    public class MediaDefaultFolder : Entity
    {
        [JsonProperty(PropertyName = "entity", NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }

        [JsonProperty(PropertyName = "folder", NullValueHandling = NullValueHandling.Ignore)]
        public MediaFolder Folder { get; set; }
    }
}
