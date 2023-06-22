using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware property group option entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/a7279a815b289-property-group-option">Property Group Option on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "property_group_option", api: "property-group-option")]
    public class PropertyGroupOption : Entity
    {
        [JsonProperty(PropertyName = "groupId", Required = Required.Always)]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long? Position { get; set; }

        [JsonProperty(PropertyName = "colorHexCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ColorHexCode { get; set; }

        [JsonProperty(PropertyName = "mediaId", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaId { get; set; }

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
                if (!string.IsNullOrWhiteSpace(this.GroupId))
                {
                    result += " GroupId: " + this.GroupId;
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
