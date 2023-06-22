using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class AccessTokenRequest
    {
        /// <summary>
        /// Liefert oder setzt die Freigabetyp des Access Token.
        /// </summary>
        /// <value>Der Typ der Freigabe des angeforderten Access Token.</value>
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Liefert oder setzt die Client ID.
        /// </summary>
        /// <value>Die Client ID.</value>
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Liefert oder setzt den Schlüssel.
        /// </summary>
        /// <value>Der Schlüssel.</value>
        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }
    }
}
