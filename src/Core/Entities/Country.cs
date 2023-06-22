using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware country entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/e7b60ac9bec5e-country">Country Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "country", api: "country")]
    public class Country : Entity
    {
        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty(PropertyName = "checkVatIdPattern", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CheckVatIdPattern { get; set; }

        [JsonProperty(PropertyName = "companyTaxFree", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CompanyTaxFree { get; set; }

        [JsonProperty(PropertyName = "displayStateInRegistration", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisplayStateInRegistration { get; set; }

        [JsonProperty(PropertyName = "forceStateInRegistration", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ForceStateInRegistration { get; set; }
        
        [JsonProperty(PropertyName = "iso", NullValueHandling = NullValueHandling.Ignore)]
        public string ISO { get; set; }

        [JsonProperty(PropertyName = "iso3", NullValueHandling = NullValueHandling.Ignore)]
        public string ISO3 { get; set; }
        
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long? Position { get; set; }

        [JsonProperty(PropertyName = "shippingAvailable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShippingAvailable { get; set; }

        [JsonProperty(PropertyName = "taxFree", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TaxFree { get; set; }

        [JsonProperty(PropertyName = "vatIdPattern", NullValueHandling = NullValueHandling.Ignore)]
        public string VatIdPattern { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.ISO ?? base.ToString();
        }
    }
}
