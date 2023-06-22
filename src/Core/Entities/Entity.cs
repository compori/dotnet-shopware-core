using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Liefert oder setzt die ID der Shopware Entity.
        /// </summary>
        /// <value>Die Shopware Entity.</value>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        
        /// <summary>
        /// Liefert oder setzt die Version ID der Shopware Entity.
        /// </summary>
        /// <value>Die Shopware Version ID.</value>
        [JsonProperty(PropertyName = "versionId", NullValueHandling = NullValueHandling.Ignore)]
        public string VersionId { get; set; }

        /// <summary>
        /// Liefert oder setzt das Erstelldatum der Entity in Shopware.
        /// </summary>
        /// <value>Das Erstelldatum der Entity.</value>
        [JsonProperty(PropertyName = "createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }
        
        /// <summary>
        /// Liefert oder setzt das Aktualisierungsdatum der Entity in Shopware.
        /// </summary>
        /// <value>Das Aktualisierungsdatum der Entity.</value>
        [JsonProperty(PropertyName = "updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Liefert oder setzt eine Liste mit Custom Fields, sofern die Entity dies unterstützt.
        /// </summary>
        /// <value>Die Liste mit Custom Fields.</value>
        [JsonProperty(PropertyName = "customFields", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> CustomFields { get; set; }
    }
}
