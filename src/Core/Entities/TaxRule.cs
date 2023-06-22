using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware tax rule entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/96d7855c14ed1-tax-rule">Tax Rule on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "tax_rule", api: "tax-rule")]
    public class TaxRule : Entity
    {
        [JsonProperty(PropertyName = "taxRuleTypeId", Required = Required.Always)]
        public string TaxRuleTypeId { get; set; }

        [JsonProperty(PropertyName = "countryId", Required = Required.Always)]
        public string CountryId { get; set; }

        [JsonProperty(PropertyName = "taxRate", Required = Required.Always)]
        public double TaxRate { get; set; }

        [JsonProperty(PropertyName = "taxId", Required = Required.Always)]
        public string TaxId { get; set; }
    }
}
