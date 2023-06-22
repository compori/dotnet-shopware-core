using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class AccessToken
    {
        /// <summary>
        /// Liefert oder setzt die Art des Tokens.
        /// </summary>
        /// <value>Die Art des Tokens.</value>
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Liefert oder setzt die Sekunden, bis der Token ungültig wird.
        /// </summary>
        /// <value>Die Sekunden, bis der Token ungültig wird.</value>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert des Token.
        /// </summary>
        /// <value>Der Wert des Token.</value>
        [JsonProperty(PropertyName = "access_token")]
        public string Value { get; set; }
    }
}
