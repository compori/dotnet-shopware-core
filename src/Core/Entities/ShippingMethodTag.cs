using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware shipping method tag entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMzM4-shipping-method-tag">Shipping Method Tag on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "shipping_method_tag", TaggableEntityIdFieldName = "shippingMethodId", TaggableTagIdFieldName = "tagId")]
    public class ShippingMethodTag : Entity, ITaggableWith
    {
        [JsonProperty(PropertyName = "shippingMethodId", Required = Required.Always)]
        public string ShippingMethodId { get; set; }

        [JsonProperty(PropertyName = "tagId", Required = Required.Always)]
        public string TagId { get; set; }

        string ITaggableWith.TagId { get => this.TagId; set => this.TagId = value; }
        string ITaggableWith.EntityId { get => this.ShippingMethodId; set => this.ShippingMethodId = value; }
    }
}
