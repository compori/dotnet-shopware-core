using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Aggregation
    {
        /// <summary>
        /// Gets or sets the identifier, so you can find it easier.
        /// </summary>
        /// <value>Give your aggregation an identifier, so you can find it easier.</value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the field you want to aggregate over.
        /// </summary>
        /// <value>The field you want to aggregate over.</value>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the type of aggregation.
        /// </summary>
        /// <value>The type of aggregation.</value>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
