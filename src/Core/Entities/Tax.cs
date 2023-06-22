using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware tax entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/a40865c8137b3-tax">Tax on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "tax", api: "tax")]
    public class Tax : Entity
    {
        [JsonProperty(PropertyName = "taxRate", Required = Required.Always)]
        public double TaxRate { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "position", Required = Required.Always)]
        public long Position { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                result = this.Name + " (" + this.TaxRate + " %)";
                if (!string.IsNullOrWhiteSpace(this.Id))
                {
                    result += " Id: " + this.Id;
                }
            }
            return result;
        }
    }
}
