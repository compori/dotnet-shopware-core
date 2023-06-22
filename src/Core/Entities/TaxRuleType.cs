using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware tax rule type entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/a008f1d723a51-tax-rule-type">Tax Rule Type on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "tax_rule_type", api: "tax-rule-type")]
    public class TaxRuleType : Entity
    {
        [JsonProperty(PropertyName = "technicalName", NullValueHandling = NullValueHandling.Ignore)]
        public string TechnicalName { get; set; }

        [JsonProperty(PropertyName = "position", Required = Required.Always)]
        public long position { get; set; }

        [JsonProperty(PropertyName = "typeName", Required = Required.Always)]
        public string TypeName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.TechnicalName))
            {
                result = this.TechnicalName;
                if (!string.IsNullOrWhiteSpace(this.TypeName))
                {
                    result += " (" + this.TypeName + ")";
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
