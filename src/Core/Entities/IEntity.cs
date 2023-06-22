namespace Compori.Shopware.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Liefert oder setzt die ID der Shopware Entity.
        /// </summary>
        /// <value>Die Shopware Entity.</value>
        string Id { get; set; }
    }
}
