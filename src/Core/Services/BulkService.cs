using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Services
{
    public class BulkService
    {
        /// <summary>
        /// Liefert den Shopware Client.
        /// </summary>
        /// <value>The client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkService"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public BulkService(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Send multiple bulk data as an asynchronous operation.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task Sync(Dictionary<string, Types.Bulk> data, CancellationToken cancellationToken = default)
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
        public async Task Sync(Types.Bulk data, CancellationToken cancellationToken = default)
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
    }
}
