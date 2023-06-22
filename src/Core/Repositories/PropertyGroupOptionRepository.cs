using Compori.Shopware.Entities;
using Compori.Shopware.Types;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Repositories
{
    public class PropertyGroupOptionRepository<TEntity> : EntityRepository<TEntity> where TEntity : PropertyGroupOption
    {
        public PropertyGroupOptionRepository(Client client) : base(client)
        {
        }

        /// <summary>
        /// Reads the options by property group identifier.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>SearchResult&lt;TEntity&gt;.</returns>
        public async Task<SearchResult<TEntity>> ReadByPropertyGroupId(string groupId, Search search = null, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(groupId, nameof(groupId));    

            search ??= new Search();
            search.Filters = new List<Filter>(search.Filters ?? Array.Empty<Filter>())
            {
                new Filter
                {
                    Field = "groupId",
                    Type = "equals",
                    Value = groupId
                }
            }.ToArray();
            return await this.Read(search, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads all options by property group identifier.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Dictionary&lt;System.String, TEntity&gt;.</returns>
        public async Task<Dictionary<string, TEntity>> ReadAllByPropertyGroupId(string groupId, Search search = null, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(groupId, nameof(groupId));    

            search ??= new Search();
            search.Filters = new List<Filter>(search.Filters ?? Array.Empty<Filter>())
            {
                new Filter
                {
                    Field = "groupId",
                    Type = "equals",
                    Value = groupId
                }
            }.ToArray();

            return await this.ReadAll(search, cancellationToken).ConfigureAwait(false);
        }
    }

    public class PropertyGroupOptionRepository : PropertyGroupOptionRepository<PropertyGroupOption>
    {
        public PropertyGroupOptionRepository(Client client) : base(client)
        {
        }
    }
}
