using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class DataResponse<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
