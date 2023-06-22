using Newtonsoft.Json;
using System;

namespace Compori.Shopware
{
    public class Settings
    {
        /// <summary>
        /// Liefert oder setzt die URL zum Shopware Shop.
        /// </summary>
        /// <value>Die URL zum Shopware Shop.</value>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Liefert oder setzt die Client ID für die Shopware API.
        /// </summary>
        /// <value>Die Client ID für die Shopware API.</value>
        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Liefert oder setzt die Client Schlüssel für die Shopware API.
        /// </summary>
        /// <value>Die Client Schlüssel für die Shopware API.</value>
        [JsonProperty(PropertyName = "clientKey")]
        public string ClientKey { get; set; }

        /// <summary>
        /// Liefert oder setzt den HTTP Agent für die Shopware API.
        /// </summary>
        /// <value>Der HTTP Agent für die Shopware API.</value>
        [JsonProperty(PropertyName = "clientAgent")]
        public string ClientAgent { get; set; }

        /// <summary>
        /// Liefert den Standard Timeout Wert für die Verbindung.
        /// </summary>
        /// <value>Der Standard Timeout Wert für die Verbindung.</value>
        public static TimeSpan DefaultTimeout => new TimeSpan(0, 5, 0);

        /// <summary>
        /// Liefert den Timeout Wert für die Verbindung.
        /// </summary>
        /// <value>Der Timeout Wert für die Verbindung.</value>
        [JsonProperty(PropertyName = "timeOut")]
        public TimeSpan Timeout { get; set; } = DefaultTimeout;

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.1 verwendet werden darf.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.1 verwendet werden darf; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "enableTls11")]
        public bool EnableTls11 { get; set; }

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.2 verwendet werden darf.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.2 verwendet werden darf; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "enableTls12")]
        public bool EnableTls12 { get; set; }

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.3 verwendet werden darf.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.3 verwendet werden darf; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "enableTls13")]
        public bool EnableTls13 { get; set; }

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.1 verwendet werden soll.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.1 verwendet werden soll; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "forceTls11")]
        public bool ForceTls11 { get; set; }

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.2 verwendet werden soll.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.2 verwendet werden soll; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "forceTls12")]
        public bool ForceTls12 { get; set; }

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.3 verwendet werden soll.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.3 verwendet werden soll; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "forceTls13")]
        public bool ForceTls13 { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Wert, der angibt ob die Anfragen und Antworten in protokolliert werden sollen.
        /// </summary>
        /// <value><c>true</c> wenn Anfragen und Antworten protokolliert werden sollen; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "trace")]
        public bool Trace { get; set; }
    }
}
