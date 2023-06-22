using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware product media entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/377f12fce7558-product-media">Product Media on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product_media")]
    public class ProductMedia : Entity
    {
        [JsonProperty(PropertyName = "productId", Required = Required.Always)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId { get; set; }

        [JsonProperty(PropertyName = "mediaId", Required = Required.Always)]
        public string MediaId { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long? Position { get; set; }
    }
}
