using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Types
{
    public class OrderState
    {
        [JsonProperty(PropertyName = "sendMail")]
        public bool SendMail { get; set; }

        [JsonProperty(PropertyName = "documentIds", NullValueHandling = NullValueHandling.Include)]
        public List<string> DocumentIds { get; set; } = new List<string>();
        
        [JsonProperty(PropertyName = "mediaIds", NullValueHandling = NullValueHandling.Include)]
        public List<string> MediaIds { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "stateFieldName", NullValueHandling = NullValueHandling.Include)]
        public string StateFieldName { get; set; } = "stateId";
    }
}
