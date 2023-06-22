using Compori.Shopware.Entities;
using Compori.Shopware.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Repositories
{
    public class EntityRepository
    {
        /// <summary>
        /// Liefert den Shopware Client.
        /// </summary>
        /// <value>The client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        protected EntityRepository(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Send multiple bulk data as an asynchronous operation.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected async Task Sync(Dictionary<string, Bulk> data, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(data, nameof(data));
            await this.Client.Post("_action/sync", data, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Send bulk data as an asynchronous operation.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected async Task Sync(Bulk data, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            await this.Sync(
                new Dictionary<string, Types.Bulk>
                {
                    { data.Action + "-" + data.Entity, data }
                },
                cancellationToken
            ).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="data">The entity data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async Task<string> Create(string entityRoute, IEntity data, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(entityRoute, nameof(entityRoute));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            if (string.IsNullOrWhiteSpace(data.Id))
            {
                data.Id = Guid.NewGuid().ToString("N");
            }
            await this.Client.Post(entityRoute, data, cancellationToken).ConfigureAwait(false);

            return data.Id;
        }

        /// <summary>
        /// Updates the entity data.
        /// </summary>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="data">The entity data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException">The entity id was not set.</exception>
        public async Task Update(string entityRoute, IEntity data, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(entityRoute, nameof(entityRoute));
            Guard.AssertArgumentIsNotNull(data, nameof(data));
            if (string.IsNullOrWhiteSpace(data.Id))
            {
                throw new InvalidOperationException("The entity id was not set.");
            }
            await this.Client.Patch($"{entityRoute}/{data.Id}", data, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates mass entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task Update(string entityName, List<IEntity> entities, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(entityName, nameof(entityName));
            Guard.AssertArgumentIsNotNull(entities, nameof(entities));
            foreach (var entity in entities)
            {
                if (string.IsNullOrWhiteSpace(entity.Id))
                {
                    entity.Id = Guid.NewGuid().ToString("N");
                }
            }
            await this.Sync(new BulkUpsert<IEntity>(entityName, entities), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a specific entity.
        /// </summary>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="id">The entity id.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task Delete(string entityRoute, string id, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(entityRoute, nameof(entityRoute));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));

            await this.Client.Delete($"{entityRoute}/{id}", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes mass entities.
        /// </summary>
        /// <param name="ids">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task Delete(string entityName, List<string> ids, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(entityName, nameof(entityName));
            Guard.AssertArgumentIsNotNull(ids, nameof(ids));

            await this.Sync(new BulkDelete(entityName, ids), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads an entity from its id.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TEntity&gt; representing the asynchronous operation.</returns>
        public async Task<TEntity> Read<TEntity>(string entityRoute, string id, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            Guard.AssertArgumentIsNotNull(entityRoute, nameof(entityRoute));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));

            var result = await this.Client.Get<DataResponse<TEntity>>($"{entityRoute}/{id}", cancellationToken).ConfigureAwait(false);

            return result == null ? default : result.Data;
        }

        /// <summary>
        /// Reads the property from a entity.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of property.</typeparam>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="id">The entity id.</param>
        /// <param name="property">Die Eigenschaft der Entity.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>T.</returns>
        public async Task<TPropertyType> ReadProperty<TPropertyType>(string entityRoute, string id, string property, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(entityRoute, nameof(entityRoute));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(property, nameof(property));

            var result = await this.Client.Get<DataResponse<TPropertyType>>($"{entityRoute}/{id}/{property}", cancellationToken).ConfigureAwait(false);
            return result == null ? default : result.Data;
        }

        /// <summary>
        /// Reads all linked entities for a property of an entity.
        /// </summary>
        /// <typeparam name="TLinkedEntity">The type of linked entity.</typeparam>
        /// <param name="id">The entity id.</param>
        /// <param name="property">The property.</param>
        /// <returns>List&lt;TLinkedEntity&gt;.</returns>
        public async Task<List<TLinkedEntity>> ReadLinkedEntities<TLinkedEntity>(string entityRoute, string id, string property, CancellationToken cancellationToken = default)
            where TLinkedEntity : class, IEntity
        {
            return await this.ReadProperty<List<TLinkedEntity>>(entityRoute, id, property, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads Entities with given search parameters.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public async Task<SearchResult<TEntity>> Read<TEntity>(string entityRoute, Search search = null, CancellationToken cancellationToken = default)
             where TEntity : class, IEntity
        {
            Guard.AssertArgumentIsNotNull(entityRoute, nameof(entityRoute));

            return await this.Client.Post<Search, SearchResult<TEntity>>($"search/{entityRoute}", search ?? new Search(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads all result of the entity without paging. This leead in several request. Each request with be limited by the search parameter limit.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <param name="entityRoute">The entity route.</param>
        /// <param name="search">The search parameter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<Dictionary<string, TEntity>> ReadAll<TEntity>(string entityRoute, Search search = null, CancellationToken cancellationToken = default)
             where TEntity : class, IEntity
        {
            search ??= new Search();
            search.Page = 1;
            search.TotalCountMode = TotalCountMode.Exact;

            var result = await this.Read<TEntity>(entityRoute, search, cancellationToken).ConfigureAwait(false);
            if (result?.Items == null || result.Items.Count == 0)
            {
                return new Dictionary<string, TEntity>();
            }
            var list = new Dictionary<string, TEntity>();
            result.Items.ForEach(v => { if (!list.ContainsKey(v.Id)) { list.Add(v.Id, v); } });

            var pages = result.Total / search.Limit;

            for (var page = 0; page < pages; page++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                search.Page = page + 2;
                result = await this.Read<TEntity>(entityRoute, search, cancellationToken).ConfigureAwait(false);
                if (result?.Items == null || result.Items.Count == 0)
                {
                    break;
                }
                result.Items.ForEach(v => { if (!list.ContainsKey(v.Id)) { list.Add(v.Id, v); } });
            }
            return list;
        }

        /// <summary>
        /// Reads entity ids with search parameter.
        /// </summary>
        /// <param name="search">The search parameter.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public async Task<SearchResult<string>> ReadIds(string entityRoute, Search search = null, CancellationToken cancellationToken = default)
        {
            return await this.Client.Post<Search, SearchResult<string>>($"search-ids/{entityRoute}", search ?? new Search(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads all result of the entity without paging. This leead in several request. Each request with be limited by the search parameter limit.
        /// </summary>
        /// <param name="search">The search parameter.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public async Task<List<string>> ReadAllIds(string entityRoute, Search search = null, CancellationToken cancellationToken = default)
        {
            search ??= new Search();
            search.Page = 1;
            search.TotalCountMode = TotalCountMode.Exact;

            var result = await this.ReadIds(entityRoute, search, cancellationToken).ConfigureAwait(false);
            if (result?.Items == null || result.Items.Count == 0)
            {
                return new List<string>();
            }
            var list = new HashSet<string>(result.Items);

            var pages = result.Total / search.Limit;

            for (var page = 0; page < pages; page++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                search.Page = page + 2;
                result = await this.ReadIds(entityRoute, search, cancellationToken).ConfigureAwait(false);
                if (result?.Items == null || result.Items.Count == 0)
                {
                    break;
                }
                result.Items.ForEach(v => list.Add(v));
            }
            return list.ToList();
        }
    }
}
