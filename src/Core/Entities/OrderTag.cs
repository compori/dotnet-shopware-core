using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order tag entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMjc4-order-tag">Order Tag Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_tag", api: "order-tag")]
    public class OrderTag : Entity
    {
        [JsonProperty(PropertyName = "orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "tagId", NullValueHandling = NullValueHandling.Ignore)]
        public string TagId { get; set; }
    }
}
