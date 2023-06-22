using System;
using System.Collections.Generic;
using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order delivery entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/b5506f0454cfc-order-delivery">Order Delivery Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_delivery", api: "order-delivery")]
    public class OrderDelivery : Entity
    {
        [JsonProperty(PropertyName = "orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "trackingCodes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TrackingCodes  { get; set; }
        
        [JsonProperty(PropertyName = "stateId", NullValueHandling = NullValueHandling.Ignore)]
        public string StateId  { get; set; }

        [JsonProperty(PropertyName = "stateMachineState", NullValueHandling = NullValueHandling.Ignore)]
        public StateMachineState StateMachineState  { get; set; }     

        [JsonProperty(PropertyName = "shippingOrderAddressId", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippingOrderAddressId  { get; set; }

        [JsonProperty(PropertyName = "shippingMethodId", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippingMethodId  { get; set; }

        [JsonProperty(PropertyName = "shippingDateLatest", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ShippingDateLatest  { get; set; }
        
        [JsonProperty(PropertyName = "shippingDateEarliest", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ShippingDateEarliest { get; set; }

        [JsonProperty(PropertyName = "shippingOrderAddress", NullValueHandling = NullValueHandling.Ignore)]
        public OrderAddress ShippingOrderAddress { get; set; }

        [JsonProperty(PropertyName = "shippingMethod", NullValueHandling = NullValueHandling.Ignore)]
        public ShippingMethod ShippingMethod { get; set; }
    }
}
