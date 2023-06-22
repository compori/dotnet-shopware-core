using Compori.Shopware.Attributes;
using Compori.Shopware.Types;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order transaction entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/01f2ced432963-order-transaction">Order Transaction Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_transaction", api: "order-transaction")]
    public class OrderTransaction : Entity
    {
        [JsonProperty(PropertyName = "orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "orderVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderVersionId { get; set; }

        [JsonProperty(PropertyName = "stateId", NullValueHandling = NullValueHandling.Ignore)]
        public string StateId  { get; set; }

        [JsonProperty(PropertyName = "paymentMethodId", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentMethodId  { get; set; }

        [JsonProperty(PropertyName = "stateMachineState", NullValueHandling = NullValueHandling.Ignore)]
        public StateMachineState StateMachineState  { get; set; }
        
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public OrderTransactionAmount Amount  { get; set; }

        [JsonProperty(PropertyName = "paymentMethod", NullValueHandling = NullValueHandling.Ignore)]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
