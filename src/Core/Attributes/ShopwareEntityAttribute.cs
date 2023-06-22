using Compori.Shopware.Entities;
using System;

namespace Compori.Shopware.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShopwareEntityAttribute : Attribute
    {
        /// <summary>
        /// Returns the Shopware entity name.
        /// </summary>
        /// <value>The Shopware entity name.</value>
        public string Name { get; private set; }
        
        /// <summary>
        /// Returns the Shopware API Path.
        /// </summary>
        /// <value>The Shopware API Path.</value>
        public string Api { get; private set; }

        /// <summary>
        /// Gets or sets the name of the taggable entity identifier field.
        /// </summary>
        /// <value>The name of the taggable entity identifier field.</value>
        public string TaggableEntityIdFieldName { get; set; }

        /// <summary>
        /// Gets or sets the name of the taggable entity identifier field.
        /// </summary>
        /// <value>The name of the taggable entity identifier field.</value>
        public string TaggableTagIdFieldName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopwareEntityAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="api">The API path.</param>
        public ShopwareEntityAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopwareEntityAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="api">The API path.</param>
        public ShopwareEntityAttribute(string name, string api) : this(name)
        {
            this.Api = api;
        }

        /// <summary>
        /// Gets the entity name for a type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>System.String.</returns>
        public static string GetName<TEntity>() where TEntity : IEntity
        {
            return GetAttribute<TEntity>()?.Name;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>System.String.</returns>
        public static string GetApi<TEntity>() where TEntity : IEntity
        {
            return GetAttribute<TEntity>()?.Api;
        }

        /// <summary>
        /// Gets the entity name for a type.
        /// </summary>
        /// <typeparam name="TTaggableWith">The type of the taggable entity.</typeparam>
        /// <returns>System.String.</returns>
        public static string GetTaggableEntityIdFieldName<TTaggableWith>() where TTaggableWith : ITaggableWith
        {
            return GetAttribute<TTaggableWith>()?.TaggableEntityIdFieldName;
        }

        /// <summary>
        /// Gets the fieldname for tag id.
        /// </summary>
        /// <typeparam name="TTaggableWith">The type of the taggable entity.</typeparam>
        /// <returns>System.String.</returns>
        public static string GetTaggableTagIdFieldName<TTaggableWith>() where TTaggableWith : ITaggableWith
        {
            return (GetAttribute<TTaggableWith>()?.TaggableTagIdFieldName) ?? "tagId";
        }
        
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>ShopwareEntityAttribute.</returns>
        public static ShopwareEntityAttribute GetAttribute<TEntity>() where TEntity : IEntity
        {
            return GetCustomAttribute(typeof(TEntity), typeof (ShopwareEntityAttribute), true) as ShopwareEntityAttribute;
        }
    }
}
