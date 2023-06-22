using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware app action button entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/8a1c36aa81b70-app-action-button">App Action Button Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "app_action_button", api: "app-action-button")]
    public class AppActionButton : Entity
    {
        [JsonProperty(PropertyName = "entity", NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }

        [JsonProperty(PropertyName = "view", NullValueHandling = NullValueHandling.Ignore)]
        public string View { get; set; }

        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "appId", NullValueHandling = NullValueHandling.Ignore)]
        public string AppId { get; set; }

    }
}
