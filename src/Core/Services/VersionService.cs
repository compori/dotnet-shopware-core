﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Services
{
    public class VersionService
    {
        /// <summary>
        /// Liefert den Shopware Client.
        /// </summary>
        /// <value>The client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionService"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public VersionService(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Get version as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;Version&gt; representing the asynchronous operation.</returns>
        public async Task<Types.Version> GetVersionAsync(CancellationToken cancellationToken = default)
        {
            return await this.Client.Get<Types.Version>("/_info/version", cancellationToken).ConfigureAwait(false);
        }
    }
}