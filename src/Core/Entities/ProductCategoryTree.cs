using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware product category tree entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMjg0-product-category-tree">Product Category Tree on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product_category_tree")]
    public class ProductCategoryTree : Entity
    {
        [JsonProperty(PropertyName = "productId", Required = Required.Always)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId { get; set; }

        [JsonProperty(PropertyName = "categoryId", Required = Required.Always)]
        public string CcategoryId { get; set; }

        [JsonProperty(PropertyName = "categoryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryVersionId { get; set; }

    }
}
