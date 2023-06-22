using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware media tag entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMjYy-media-tag">Media Tag Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "media_tag", api: "media-tag")]
    public class MediaTag : Entity
    {
        [JsonProperty(PropertyName = "mediaId", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaId { get; set; }

        [JsonProperty(PropertyName = "tagId", NullValueHandling = NullValueHandling.Ignore)]
        public string TagId { get; set; }
    }
}
