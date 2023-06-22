using System;
using System.Collections.Generic;
using Compori.Shopware.Attributes;
using Compori.Shopware.Types;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/991f88e90b0d6-order">Order Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order", api: "order")]
    public class Order : Entity
    {
        [JsonProperty(PropertyName = "orderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "billingAddressId", NullValueHandling = NullValueHandling.Ignore)]
        public string BillingAddressId { get; set; }

        [JsonProperty(PropertyName = "billingAddress", NullValueHandling = NullValueHandling.Ignore)]
        public OrderAddress BillingAddress { get; set; }

        [JsonProperty(PropertyName = "currencyId", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyId { get; set; }
        
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public Currency Currency { get; set; }

        [JsonProperty(PropertyName = "languageId", NullValueHandling = NullValueHandling.Ignore)]
        public string LanguageId { get; set; }

        [JsonProperty(PropertyName = "salesChannelId", NullValueHandling = NullValueHandling.Ignore)]
        public string SalesChannelId { get; set; }

        [JsonProperty(PropertyName = "orderDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime OrderDateTime { get; set; }
        
        [JsonProperty(PropertyName = "orderDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime OrderDate { get; set; }
        
        [JsonProperty(PropertyName = "currencyFactor", NullValueHandling = NullValueHandling.Ignore)]
        public double CurrencyFactor { get; set; }

        [JsonProperty(PropertyName = "stateId", NullValueHandling = NullValueHandling.Ignore)]
        public string StateId { get; set; }

        [JsonProperty(PropertyName = "stateMachineState", NullValueHandling = NullValueHandling.Ignore)]
        public StateMachineState StateMachineState  { get; set; }

        [JsonProperty(PropertyName = "price", NullValueHandling = NullValueHandling.Ignore)]
        public OrderPrice Price { get; set; }

        [JsonProperty(PropertyName = "amountTotal", NullValueHandling = NullValueHandling.Ignore)]
        public double? AmountTotal { get; set; }

        [JsonProperty(PropertyName = "amountNet", NullValueHandling = NullValueHandling.Ignore)]
        public double? AmountNet { get; set; }
        
        [JsonProperty(PropertyName = "positionPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? PositionPrice { get; set; }
            
        [JsonProperty(PropertyName = "taxStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxStatus { get; set; }

        [JsonProperty(PropertyName = "shippingTotal", NullValueHandling = NullValueHandling.Ignore)]
        public double ShippingTotal { get; set; }
        
        [JsonProperty(PropertyName = "customerComment", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerComment { get; set; }
        
        [JsonProperty(PropertyName = "affiliateCode", NullValueHandling = NullValueHandling.Ignore)]
        public string AffiliateCode { get; set; }
        
        [JsonProperty(PropertyName = "campaignCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CampaignCode { get; set; }

        [JsonProperty(PropertyName = "shippingCosts", NullValueHandling = NullValueHandling.Ignore)]
        public OrderShippingCosts ShippingCosts { get; set; }

        [JsonProperty(PropertyName = "lineItems", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderLineItem> LineItems { get; set; }

        [JsonProperty(PropertyName = "orderCustomer", NullValueHandling = NullValueHandling.Ignore)]
        public OrderCustomer OrderCustomer { get; set; }
        
        [JsonProperty(PropertyName = "deliveries", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderDelivery> Deliveries { get; set; }
        
        [JsonProperty(PropertyName = "transactions" , NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderTransaction> Transactions { get; set; }
    }
}
