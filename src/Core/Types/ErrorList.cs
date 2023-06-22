using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class ErrorList
    {
        /// <summary>
        /// Liefert eine Auflistung von Fehlern zurück.
        /// </summary>
        /// <value>The errors.</value>
        [JsonProperty(PropertyName = "errors")]
        public Error[] Errors { get; set; }
    }
}
