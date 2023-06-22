namespace Compori.Shopware.Entities
{
    public interface ITaggableWith : IEntity
    {
        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        public string EntityId { get; set; }

        /// <summary>
        /// Gets the tag identifier.
        /// </summary>
        /// <value>The tag identifier.</value>
        public string TagId { get; set; }
    }
}
