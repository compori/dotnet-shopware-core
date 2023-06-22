using System.Collections.Generic;
using Compori.Shopware.Attributes;
using Compori.Shopware.Types;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order line item entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/48f505dd21f65-order-line-item">Order Line Item Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_line_item", api: "order-line-item")]
    public class OrderLineItem : Entity
    {
        [JsonProperty(PropertyName = "coverId", NullValueHandling = NullValueHandling.Ignore)]
        public string CoverId { get; set; }
        
        [JsonProperty(PropertyName = "parentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }

        [JsonProperty(PropertyName = "parentVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentVersionId { get; set; }

        [JsonProperty(PropertyName = "orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "orderVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderVersionId { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string Identifier { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long Position { get; set; }

        [JsonProperty(PropertyName = "productId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId { get; set; }

        [JsonProperty(PropertyName = "referencedId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReferencedId { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quantity { get; set; }

        [JsonProperty(PropertyName = "totalPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalPrice { get; set; }

        [JsonProperty(PropertyName = "unitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? UnitPrice { get; set; }

        [JsonProperty(PropertyName = "stackable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Stackable { get; set; }

        [JsonProperty(PropertyName = "removable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Removable { get; set; }

        [JsonProperty(PropertyName = "good", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Good { get; set; }

        [JsonProperty(PropertyName = "price", NullValueHandling = NullValueHandling.Ignore)]
        public OrderLineItemPrice Price { get; set; }

        [JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Payload { get; set; }

        [JsonProperty(PropertyName = "product", NullValueHandling = NullValueHandling.Ignore)]
        public Product Product { get; set; }
    }
}
