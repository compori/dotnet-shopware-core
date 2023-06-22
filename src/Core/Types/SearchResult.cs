using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class SearchResult<T>
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
        
        [JsonProperty(PropertyName = "data")]
        public List<T> Items { get; set; }
    }
}
