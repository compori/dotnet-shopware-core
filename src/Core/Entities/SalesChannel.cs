using Compori.Shopware.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware sales channel entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/a441020a67bb8-sales-channel">Sales Channel on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "sales_channel", api: "sales-channel")]
    public class SalesChannel : Entity
    {
        [JsonProperty(PropertyName = "typeId", Required = Required.Always)]
        public string TypeId { get; set; }

        [JsonProperty(PropertyName = "languageId", Required = Required.Always)]
        public string LanguageId { get; set; }

        [JsonProperty(PropertyName = "customerGroupId", Required = Required.Always)]
        public string CustomerGroupId { get; set; }

        [JsonProperty(PropertyName = "currencyId", Required = Required.Always)]
        public string CurrencyId { get; set; }

        [JsonProperty(PropertyName = "paymentMethodId", Required = Required.Always)]
        public string PaymentMethodId { get; set; }

        [JsonProperty(PropertyName = "shippingMethodId", Required = Required.Always)]
        public string ShippingMethodId { get; set; }

        [JsonProperty(PropertyName = "countryId", Required = Required.Always)]
        public string CountryId { get; set; }

        [JsonProperty(PropertyName = "analyticsId", NullValueHandling = NullValueHandling.Ignore)]
        public string AnalyticsId { get; set; }

        [JsonProperty(PropertyName = "navigationCategoryId", Required = Required.Always)]
        public string NavigationCategoryId { get; set; }

        [JsonProperty(PropertyName = "navigationCategoryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string NavigationCategoryVersionId { get; set; }

        [JsonProperty(PropertyName = "navigationCategoryDepth", NullValueHandling = NullValueHandling.Ignore)]
        public long? NavigationCategoryDepth { get; set; }

        [JsonProperty(PropertyName = "footerCategoryId", NullValueHandling = NullValueHandling.Ignore)]
        public string FooterCategoryId { get; set; }

        [JsonProperty(PropertyName = "footerCategoryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string FooterCategoryVersionId { get; set; }

        [JsonProperty(PropertyName = "serviceCategoryId", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceCategoryId { get; set; }

        [JsonProperty(PropertyName = "serviceCategoryVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceCategoryVersionId { get; set; }

        [JsonProperty(PropertyName = "mailHeaderFooterId", NullValueHandling = NullValueHandling.Ignore)]
        public string MailHeaderFooterId { get; set; }

        [JsonProperty(PropertyName = "hreflangDefaultDomainId", NullValueHandling = NullValueHandling.Ignore)]
        public string HreflangDefaultDomainId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "shortName", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "taxCalculationType", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxCalculationType { get; set; }

        [JsonProperty(PropertyName = "accessKey", Required = Required.Always)]
        public string AccessKey { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty(PropertyName = "hreflangActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HreflangActive { get; set; }

        [JsonProperty(PropertyName = "maintenance", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Maintenance { get; set; }

        [JsonProperty(PropertyName = "paymentMethodIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> PaymentMethodIds { get; set; }

        [JsonProperty(PropertyName = "homeCmsPageId", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCmsPageId { get; set; }

        [JsonProperty(PropertyName = "homeCmsPageVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCmsPageVersionId { get; set; }

        [JsonProperty(PropertyName = "homeEnabled", Required = Required.Always)]
        public bool HomeEnabled { get; set; }

        [JsonProperty(PropertyName = "homeName", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeName { get; set; }

        [JsonProperty(PropertyName = "homeMetaTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeMetaTitle { get; set; }

        [JsonProperty(PropertyName = "homeMetaDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeMetaDescription { get; set; }

        [JsonProperty(PropertyName = "homeKeywords", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeKeywords { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                result = this.Name;
                if (!string.IsNullOrWhiteSpace(this.AccessKey))
                {
                    result += " (" + this.AccessKey + ")";
                }
                if (!string.IsNullOrWhiteSpace(this.Id))
                {
                    result += " Id: " + this.Id;
                }
            }
            return result;
        }
    }
}
