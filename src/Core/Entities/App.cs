using Compori.Shopware.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware app entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/90786913825aa-app">App Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "app", api: "app")]
    public class App : Entity
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }
        
        [JsonProperty(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }
        
        [JsonProperty(PropertyName = "copyright", NullValueHandling = NullValueHandling.Ignore)]
        public string Copyright { get; set; }
        
        [JsonProperty(PropertyName = "license", NullValueHandling = NullValueHandling.Ignore)]
        public string License { get; set; }
        
        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }
        
        [JsonProperty(PropertyName = "configurable", NullValueHandling = NullValueHandling.Ignore)]
        public bool Configurable { get; set; }
        
        [JsonProperty(PropertyName = "privacy", NullValueHandling = NullValueHandling.Ignore)]
        public string Privacy { get; set; }
        
        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }
        
        [JsonProperty(PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "allowDisable", NullValueHandling = NullValueHandling.Ignore)]
        public bool allowDisable { get; set; }

        [JsonProperty(PropertyName = "baseAppUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseAppUrl { get; set; }

        [JsonProperty(PropertyName = "allowedHosts", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AllowedHosts { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "privacyPolicyExtensions", NullValueHandling = NullValueHandling.Ignore)]
        public string PrivacyPolicyExtensions { get; set; }

        [JsonProperty(PropertyName = "integrationId", NullValueHandling = NullValueHandling.Ignore)]
        public string IntegrationId { get; set; }

        [JsonProperty(PropertyName = "aclRoleId", NullValueHandling = NullValueHandling.Ignore)]
        public string AclRoleId { get; set; }
    }
}
