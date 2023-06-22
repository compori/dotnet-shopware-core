using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware product option entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMjk0-product-option">Product Option on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product_option")]
    public class ProductOption : Entity
    {
        [JsonProperty(PropertyName = "productId", Required = Required.Always)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId { get; set; }

        [JsonProperty(PropertyName = "optionId", Required = Required.Always)]
        public string OptionId { get; set; }
    }
}
