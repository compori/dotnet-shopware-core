using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Error
    {
        /// <summary>
        /// Gets or sets a unique identifier for this particular occurrence of the problem..
        /// </summary>
        /// <value>A unique identifier for this particular occurrence of the problem.</value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code applicable to this problem, expressed as a string value.
        /// </summary>
        /// <value>The HTTP status code applicable to this problem, expressed as a string value.</value>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets an application-specific error code, expressed as a string value.
        /// </summary>
        /// <value>An application-specific error code, expressed as a string value.</value>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a short, human-readable summary of the problem. It **SHOULD NOT** change from occurrence to occurrence of the problem, except for purposes of localization.
        /// </summary>
        /// <value>A short, human-readable summary of the problem. It **SHOULD NOT** change from occurrence to occurrence of the problem, except for purposes of localization.</value>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        /// <value>A human-readable explanation specific to this occurrence of the problem.</value>
        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }

        [JsonProperty(PropertyName = "source")]
        public ErrorSource Source { get; set; }
    }
}
