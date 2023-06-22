using System;
using System.Collections.Generic;
using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware currency entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/5879e5c7d656b-customer">Currency Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "customer", api: "customer")]
    public class Customer : Entity
    {
        [JsonProperty(PropertyName = "groupId", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "group", NullValueHandling = NullValueHandling.Ignore)]
        public CustomerGroup Group { get; set; }

        [JsonProperty(PropertyName = "defaultPaymentMethodId", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultPaymentMethodId { get; set; }

        [JsonProperty(PropertyName = "defaultPaymentMethod", NullValueHandling = NullValueHandling.Ignore)]
        public PaymentMethod DefaultPaymentMethod { get; set; }

        [JsonProperty(PropertyName = "salesChannelId", NullValueHandling = NullValueHandling.Ignore)]
        public string SalesChannelId { get; set; }

        [JsonProperty(PropertyName = "languageId", NullValueHandling = NullValueHandling.Ignore)]
        public string LanguageId { get; set; }

        [JsonProperty(PropertyName = "lastPaymentMethodId", NullValueHandling = NullValueHandling.Ignore)]
        public string LastPaymentMethodId { get; set; }

        [JsonProperty(PropertyName = "defaultBillingAddressId", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultBillingAddressId { get; set; }

        [JsonProperty(PropertyName = "defaultBillingAddress", NullValueHandling = NullValueHandling.Ignore)]
        public CustomerAddress DefaultBillingAddress { get; set; }

        [JsonProperty(PropertyName = "defaultShippingAddressId", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultShippingAddressId { get; set; }

        [JsonProperty(PropertyName = "defaultShippingAddress", NullValueHandling = NullValueHandling.Ignore)]
        public CustomerAddress DefaultShippingAddress { get; set; }

        [JsonProperty(PropertyName = "addresses", NullValueHandling = NullValueHandling.Ignore)]
        public List<CustomerAddress> Addresses { get; set; }

        [JsonProperty(PropertyName = "customerNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerNumber { get; set; }

        [JsonProperty(PropertyName = "salutationId", NullValueHandling = NullValueHandling.Ignore)]
        public string SalutationId { get; set; }

        [JsonProperty(PropertyName = "salutation", NullValueHandling = NullValueHandling.Ignore)]
        public Salutation Salutation { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "company", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "vatIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> VatIds { get; set; }

        [JsonProperty(PropertyName = "affiliateCode", NullValueHandling = NullValueHandling.Ignore)]
        public string AffiliateCode { get; set; }

        [JsonProperty(PropertyName = "campaignCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CampaignCode { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "doubleOptInRegistration", NullValueHandling = NullValueHandling.Ignore)]
        public bool DoubleOptInRegistration { get; set; }
        
        [JsonProperty(PropertyName = "doubleOptInEmailSentDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DoubleOptInEmailSentDate { get; set; }

        [JsonProperty(PropertyName = "doubleOptInConfirmDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DoubleOptInConfirmDate { get; set; }

        [JsonProperty(PropertyName = "hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }
        
        [JsonProperty(PropertyName = "guest", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Guest { get; set; }

        [JsonProperty(PropertyName = "firstLogin", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? FirstLogin { get; set; }

        [JsonProperty(PropertyName = "lastLogin", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastLogin { get; set; }
        
        [JsonProperty(PropertyName = "newsletter", NullValueHandling = NullValueHandling.Ignore)]
        public bool Newsletter { get; set; }

        [JsonProperty(PropertyName = "birthday", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Birthday { get; set; }

        [JsonProperty(PropertyName = "lastOrderDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastOrderDate { get; set; }

        [JsonProperty(PropertyName = "remoteAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string RemoteAddress { get; set; }
    }
}
