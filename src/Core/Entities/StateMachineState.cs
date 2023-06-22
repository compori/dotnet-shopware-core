using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware state machine state entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/8af19ce5b5a52-state-machine-state">State Machine State on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "state_machine_state", api: "state-machine-state")]
    public class StateMachineState : Entity
    {
        [JsonProperty(PropertyName = "technicalName", Required = Required.Always)]
        public string TechnicalName { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "stateMachineId", Required = Required.Always)]
        public string StateMachineId { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.TechnicalName))
            {
                result = this.TechnicalName;
                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    result += " (" + this.Name + ")";
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
