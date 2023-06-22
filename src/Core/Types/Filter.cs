using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Filter
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }
        
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
