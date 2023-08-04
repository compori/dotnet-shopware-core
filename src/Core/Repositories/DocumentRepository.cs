using Compori.Shopware.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Compori.Shopware.Repositories
{
    public class DocumentRepository<TEntity> : EntityRepository<TEntity> where TEntity : Document
    {
        public DocumentRepository(Client client) : base(client)
        {
        }

        /// <summary>
        /// Uploads any binary data to a document entity.
        /// </summary>
        /// <param name="id">Die Entity ID.</param>
        /// <param name="data">The data.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task Upload(string id, byte[] data, string mimeType, string fileName, string fileExtension, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNull(data, nameof(data));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(mimeType, nameof(mimeType));

            var uri = $"/_action/{this.EntityRoute}/{id}/upload";

            var parameters = new List<string>();
            if (!string.IsNullOrWhiteSpace(fileExtension))
            {
                parameters.Add("extension=" + Uri.EscapeDataString(fileExtension));
            }
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                parameters.Add("fileName=" + Uri.EscapeDataString(fileName));
            }
            if (parameters.Count > 0)
            {
                uri += "?" + string.Join("&", parameters);
            }

            await this.Client.PostBytes(uri, data, null, null, fileExtension, mimeType, cancellationToken);
        }
    }

    public class DocumentRepository : DocumentRepository<Document>
    {
        public DocumentRepository(Client client) : base(client)
        {
        }
    }
}