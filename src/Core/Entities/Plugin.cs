using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware plugin entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/bcfb104834043-plugin">Plugin on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "plugin", api: "plugin")]
    public class Plugin : Entity
    {
        [JsonProperty(PropertyName = "baseClass", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseClass { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "composerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ComposerName { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "managedByComposer", NullValueHandling = NullValueHandling.Ignore)]
        public bool ManagedByComposer { get; set; }

        [JsonProperty(PropertyName = "path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "copyright", NullValueHandling = NullValueHandling.Ignore)]
        public string Copyright { get; set; }

        [JsonProperty(PropertyName = "license", NullValueHandling = NullValueHandling.Ignore)]
        public string License { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "upgradeVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string UpgradeVersion { get; set; }

        [JsonProperty(PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "manufacturerLink", NullValueHandling = NullValueHandling.Ignore)]
        public string ManufacturerLink { get; set; }

        [JsonProperty(PropertyName = "supportLink", NullValueHandling = NullValueHandling.Ignore)]
        public string SupportLink { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                result = this.Name;
                if (!string.IsNullOrWhiteSpace(this.ComposerName))
                {
                    result += " (" + this.ComposerName + ")";
                    if (!string.IsNullOrWhiteSpace(this.Version))
                    {
                        result += " - " + this.Version;
                    }
                }
                if(!string.IsNullOrWhiteSpace(this.Id))
                {
                    result += " " + this.Id;
                }
            }
            return result;
        }
    }
}
