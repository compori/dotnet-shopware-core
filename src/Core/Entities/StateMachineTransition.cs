using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware state machine transition entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/d2d3b29ba06a8-state-machine-transition">State Machine Transition on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "state_machine_transition", api: "state-machine-transition")]
    public class StateMachineTransition : Entity
    {
        [JsonProperty(PropertyName = "actionName", Required = Required.Always)]
        public string ActionName { get; set; }

        [JsonProperty(PropertyName = "stateMachineId", Required = Required.Always)]
        public string StateMachineId { get; set; }

        [JsonProperty(PropertyName = "fromStateId", Required = Required.Always)]
        public string FromStateId { get; set; }

        [JsonProperty(PropertyName = "toStateId", Required = Required.Always)]
        public string ToStateId { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.ActionName))
            {
                result = this.ActionName;
                if (!string.IsNullOrWhiteSpace(this.FromStateId))
                {
                    result += " " + this.FromStateId;
                    if (!string.IsNullOrWhiteSpace(this.ToStateId))
                    {
                        result += " -> " + this.ToStateId;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.StateMachineId))
                {
                    result += " MachineId: " + this.StateMachineId;
                }
                if (!string.IsNullOrWhiteSpace(this.Id))
                {
                    result += " Id: " + this.Id;
                }
            }
            return result;
        }
    }
}
