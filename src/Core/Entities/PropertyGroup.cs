using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware property group entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/5048142b1b3d2-property-group">Property Group on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "property_group", api: "property-group")]
    public class PropertyGroup : Entity
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "displayType", Required = Required.Always)]
        public string DisplayType { get; set; }

        [JsonProperty(PropertyName = "sortingType", Required = Required.Always)]
        public string SortingType { get; set; }

        [JsonProperty(PropertyName = "filterable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Filterable { get; set; }

        [JsonProperty(PropertyName = "visibleOnProductDetailPage", NullValueHandling = NullValueHandling.Ignore)]
        public bool? VisibleOnProductDetailPage { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long? Position { get; set; }

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
                if (!string.IsNullOrWhiteSpace(this.DisplayType))
                {
                    result += " (" + this.DisplayType + ")";
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
