using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware product tag entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMzA1-product-tag">Product Tag on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product_tag", TaggableEntityIdFieldName = "productId", TaggableTagIdFieldName = "tagId")]
    public class ProductTag : Entity, ITaggableWith
    {
        [JsonProperty(PropertyName = "productId", Required = Required.Always)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId  { get; set; }

        [JsonProperty(PropertyName = "tagId", Required = Required.Always)]
        public string TagId { get; set; }

        string ITaggableWith.TagId { get => this.TagId; set => this.TagId = value; }
        string ITaggableWith.EntityId { get => this.ProductId; set => this.ProductId = value; }
    }
}
