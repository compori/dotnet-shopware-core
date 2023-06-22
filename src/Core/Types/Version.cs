using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Version
    {
        [JsonProperty(PropertyName = "version")]
        public string Value { get; set; }
    }
}
