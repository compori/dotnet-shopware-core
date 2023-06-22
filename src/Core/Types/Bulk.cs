using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public abstract class Bulk
    {
        /// <summary>
        /// Gets the entity name.
        /// </summary>
        /// <value>The entity.</value>
        [JsonProperty(PropertyName = "entity")]
        public string Entity { get; }

        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        /// <value>The action.</value>
        [JsonProperty(PropertyName = "action")]
        public string Action { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bulk"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected Bulk(string entity)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(entity, nameof(entity));

            this.Entity = entity;
        }
    }
}
