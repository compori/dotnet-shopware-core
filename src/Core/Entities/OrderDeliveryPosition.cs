using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order delivery position entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/0f29e8db95771-order-delivery-position">Order Delivery Position Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_delivery_position", api: "order-delivery-position")]
    public class OrderDeliveryPosition : Entity
    {
        [JsonProperty(PropertyName = "orderDeliveryId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDeliveryId { get; set; }

        [JsonProperty(PropertyName = "orderDeliveryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDeliveryVersionId { get; set; }

        [JsonProperty(PropertyName = "orderLineItemId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderLineItemId { get; set; }

        [JsonProperty(PropertyName = "orderLineItemVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderLineItemVersionId { get; set; }

        [JsonProperty(PropertyName = "unitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double UnitPrice { get; set; }

        [JsonProperty(PropertyName = "totalPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalPrice { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public long Quantity { get; set; }
    }
}
