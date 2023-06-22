using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware currency entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/21e947232e59f-currency">Currency Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "currency", api: "currency")]
    public class Currency : Entity
    {
        [JsonProperty(PropertyName = "factor", NullValueHandling = NullValueHandling.Ignore)]
        public double? Factor { get; set; }

        [JsonProperty(PropertyName = "isoCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ISOCode { get; set; }

        [JsonProperty(PropertyName = "symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "shortName", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long? Position { get; set; }

        [JsonProperty(PropertyName = "isSystemDefault", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSystemDefault { get; set; }
    }
}
