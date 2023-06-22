using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware customer group entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/02fedb027cf09-customer-group">Customer Group Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "customer_group", api: "customer-group")]
    public class CustomerGroup : Entity
    {
        [JsonProperty(PropertyName = "displayGross", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisplayGross { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "registrationActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RegistrationActive { get; set; }

        [JsonProperty(PropertyName = "registrationIntroduction", NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationIntroduction { get; set; }

        [JsonProperty(PropertyName = "registrationOnlyCompanyRegistration", NullValueHandling = NullValueHandling.Ignore)]
        public bool RegistrationOnlyCompanyRegistration { get; set; }

        [JsonProperty(PropertyName = "registrationSeoMetaDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationSeoMetaDescription { get; set; }

        [JsonProperty(PropertyName = "registrationTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationTitle { get; set; }
    }
}
