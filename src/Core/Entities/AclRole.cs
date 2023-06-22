using Compori.Shopware.Attributes;
using Newtonsoft.Json;
using System;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware acl role entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/d4c5dba2883e7-acl-role">Acl Role Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "acl_role", api: "acl-role")]
    public class AclRole : Entity
    {
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "deletedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeletedAt { get; set; }
    }
}
