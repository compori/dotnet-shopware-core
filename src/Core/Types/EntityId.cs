using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class EntityId
    {
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "version_id", NullValueHandling = NullValueHandling.Ignore)]
        public string VersionId { get; set; }
    }
}
