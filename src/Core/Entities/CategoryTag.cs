using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware category tag entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMjEy-category-tag">Category Tag Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "category_tag", api: "category-tag")]
    public class CategoryTag : Entity
    {
        [JsonProperty(PropertyName = "categoryId", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryId { get; set; }

        [JsonProperty(PropertyName = "categoryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryVersionId { get; set; }

        [JsonProperty(PropertyName = "tagId", NullValueHandling = NullValueHandling.Ignore)]
        public string TagId { get; set; }
    }
}
