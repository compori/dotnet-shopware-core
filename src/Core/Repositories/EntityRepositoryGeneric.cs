using Compori.Shopware.Entities;
using Compori.Shopware.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Repositories
{
    public class EntityRepository<TEntity> : EntityRepository where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the entity route.
        /// </summary>
        /// <value>The entity route.</value>
        protected string EntityRoute { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{T}"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="entityRoute">The entity route.</param>
        protected EntityRepository(Client client, string entityRoute) : base(client)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(entityRoute, nameof(entityRoute));
            this.EntityRoute = entityRoute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{T}"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="entityRoute">The entity route.</param>
        protected EntityRepository(Client client) : this(client, Attributes.ShopwareEntityAttribute.GetApi<TEntity>())
        {
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="data">The entity data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async Task<string> Create(TEntity data, CancellationToken cancellationToken = default)
        {
            return await this.Create(this.EntityRoute, data, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the entity data.
        /// </summary>
        /// <param name="data">The entity data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task Update(TEntity data, CancellationToken cancellationToken = default)
        {
            await this.Update(this.EntityRoute, data, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates mass entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task Update(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(entities, nameof(entities));
            foreach(var entity in entities)
            {
                if (string.IsNullOrWhiteSpace(entity.Id))
                {
                    entity.Id = Guid.NewGuid().ToString("N");
                }
            }
            await this.Sync(new BulkUpsert<TEntity>(entities), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a specific entity.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task Delete(string id, CancellationToken cancellationToken = default)
        {
            await this.Delete(this.EntityRoute, id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes mass entities.
        /// </summary>
        /// <param name="ids">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task Delete(List<string> ids, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(ids, nameof(ids));
            await this.Sync(new BulkDelete<TEntity>(ids), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads an entity from its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>T.</returns>
        public async Task<TEntity> Read(string id, CancellationToken cancellationToken = default)
        {
            return await this.Read<TEntity>(this.EntityRoute, id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads the property from a entity.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of property.</typeparam>
        /// <param name="id">The entity id.</param>
        /// <param name="property">Die Eigenschaft der Entity.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>T.</returns>
        public async Task<TPropertyType> ReadProperty<TPropertyType>(string id, string property, CancellationToken cancellationToken = default)
        {
            return await this.ReadProperty<TPropertyType>(this.EntityRoute, id, property, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads all linked entities for a property of an entity.
        /// </summary>
        /// <typeparam name="TLinkedEntity">The type linked entity.</typeparam>
        /// <param name="id">The entity id.</param>
        /// <param name="property">The entity property.</param>
        /// <returns>List&lt;TLinkedEntity&gt;.</returns>
        public async Task<List<TLinkedEntity>> ReadLinkedEntities<TLinkedEntity>(string id, string property, CancellationToken cancellationToken = default)
            where TLinkedEntity : class, IEntity
        {
            return await this.ReadLinkedEntities<TLinkedEntity>(this.EntityRoute, id, property, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads Entities with given search parameters.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public async Task<SearchResult<TEntity>> Read(Search search = null, CancellationToken cancellationToken = default)
        {
            return await this.Read<TEntity>(this.EntityRoute, search ?? new Search(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads all result of the entity without paging. This leead in several request. Each request with be limited by the search parameter limit.
        /// </summary>
        /// <param name="search">The search parameter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<Dictionary<string, TEntity>> ReadAll(Search search = null, CancellationToken cancellationToken = default)
        {
            return await this.ReadAll<TEntity>(this.EntityRoute, search, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads entity ids with search parameter.
        /// </summary>
        /// <param name="search">The search parameter.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public async Task<SearchResult<string>> ReadIds(Search search = null, CancellationToken cancellationToken = default)
        {
            return await this.ReadIds(this.EntityRoute, search ?? new Search(), cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Reads all result of the entity without paging. This leead in several request. Each request with be limited by the search parameter limit.
        /// </summary>
        /// <param name="search">The search parameter.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public async Task<List<string>> ReadAllIds(Search search = null, CancellationToken cancellationToken = default)
        {
            return await this.ReadAllIds(this.EntityRoute, search, cancellationToken).ConfigureAwait(false);
        }
    }
}
