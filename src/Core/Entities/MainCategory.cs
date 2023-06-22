using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware main category entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/eef81375ae475-main-category">Main Category Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "main_category", api: "main-category")]
    public class MainCategory : Entity
    {
        [JsonProperty(PropertyName = "productId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId { get; set; }

        [JsonProperty(PropertyName = "CategoryId", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryId { get; set; }

        [JsonProperty(PropertyName = "categoryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryVersionId { get; set; }

        [JsonProperty(PropertyName = "salesChannelId", NullValueHandling = NullValueHandling.Ignore)]
        public string SalesChannelId { get; set; }
    }
}
