using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware salutation entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/f6997df55e403-salutation">Salutation on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "salutation", api: "salutation")]
    public class Salutation : Entity
    {
        [JsonProperty(PropertyName = "salutationKey", Required = Required.Always)]
        public string SalutationKey { get; set; }

        [JsonProperty(PropertyName = "displayName", Required = Required.Always)]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "letterName", Required = Required.Always)]
        public string LetterName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.SalutationKey))
            {
                result = this.SalutationKey;
                if (!string.IsNullOrWhiteSpace(this.DisplayName))
                {
                    result += " (" + this.DisplayName + ")";
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
