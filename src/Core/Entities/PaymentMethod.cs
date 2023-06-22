using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware payment method entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/8851188c01b85-payment-method">Payment Method Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "payment_method", api: "payment-method")]
    public class PaymentMethod : Entity
    {
        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty(PropertyName = "distinguishableName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistinguishableName { get; set; }
        
        [JsonProperty(PropertyName = "formattedHandlerIdentifier", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedHandlerIdentifier { get; set; }
        
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "mediaId", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaId { get; set; }
        
        [JsonProperty(PropertyName = "pluginId", NullValueHandling = NullValueHandling.Ignore)]
        public string PluginId { get; set; }
        
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long Position { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.Name ?? base.ToString();
        }
    }
}
