using Compori.Shopware.Attributes;
using Compori.Shopware.Types;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware state machine history entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/9a2d77ad40c1e-state-machine-history">State Machine History on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "state_machine_history", api: "state-machine-history")]
    public class StateMachineHistory : Entity
    {
        [JsonProperty(PropertyName = "stateMachineId", Required = Required.Always)]
        public string StateMachineId { get; set; }

        [JsonProperty(PropertyName = "entityName", Required = Required.Always)]
        public string EntityName { get; set; }

        [JsonProperty(PropertyName = "entityId", Required = Required.Always)]
        public EntityId EntityId { get; set; }

        [JsonProperty(PropertyName = "fromStateId", Required = Required.Always)]
        public string FromStateId { get; set; }

        [JsonProperty(PropertyName = "toStateId", Required = Required.Always)]
        public string ToStateId { get; set; }

        [JsonProperty(PropertyName = "transitionActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string TransitionActionName { get; set; }

        [JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
    }
}
