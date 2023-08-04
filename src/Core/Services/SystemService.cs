using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Services
{
    public class SystemService
    {
        /// <summary>
        /// Gets the shopware rest client.
        /// </summary>
        /// <value>The shopware rest client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemService"/> class.
        /// </summary>
        /// <param name="client">The shopware rest client.</param>
        public SystemService(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// The container cache is immediately cleared synchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task ClearContainerCaches(CancellationToken cancellationToken = default)
        {
            await this.Client.Delete("/_action/container_cache", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// The cache is immediately cleared synchronously for all used adapters.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task ClearCaches(CancellationToken cancellationToken = default)
        {
            await this.Client.Delete("/_action/cache", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// After the cache has been cleared, new cache entries are generated asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task ClearAndWarmUpCaches(CancellationToken cancellationToken = default)
        {
            await this.Client.Delete("/_action/cache_warmup", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Removes cache folders that are not needed anymore.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task CleanUpCacheFolders(CancellationToken cancellationToken = default)
        {
            await this.Client.Delete("/_action/cleanup", cancellationToken).ConfigureAwait(false);
        }

    }
}
