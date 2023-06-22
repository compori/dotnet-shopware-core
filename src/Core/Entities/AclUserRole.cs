using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware acl user role entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/c2NoOjE0MzUxMjA2-acl-user-role">Acl User Role Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "acl_user_role", api: "acl-user-role")]
    public class AclUserRole : Entity
    {
        [JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "aclRoleId", NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public string AclRoleId { get; set; }
    }
}
