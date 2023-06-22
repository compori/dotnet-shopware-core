using System.Collections.Generic;
using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order customer entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/d12f674ee0270-order-customer">Order Customer Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_customer", api: "order-customer")]
    public class OrderCustomer : Entity
    {
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "salutationId", NullValueHandling = NullValueHandling.Ignore)]
        public string SalutationId { get; set; }
        
        [JsonProperty(PropertyName = "salutation", NullValueHandling = NullValueHandling.Ignore)]
        public Salutation Salutation { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "company", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "customerNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerNumber { get; set; }

        [JsonProperty(PropertyName = "customerId", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "customer", NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }

        [JsonProperty(PropertyName = "remoteAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string RemoteAddress { get; set; }

        [JsonProperty(PropertyName = "vatIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> VatIds { get; set; }
    }
}
