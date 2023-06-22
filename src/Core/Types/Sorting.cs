using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Sorting
    {
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }
            
        [JsonProperty(PropertyName = "naturalSorting")]
        public bool NaturalSorting { get; set; }
    }
}
