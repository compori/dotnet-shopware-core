using Compori.Shopware.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;

namespace Compori.Shopware.Repositories
{
    public class MediaRepository<TEntity> : EntityRepository<TEntity> where TEntity : Media
    {
        public MediaRepository(Client client) : base(client)
        {
        }

        /// <summary>
        /// Create a Media Item and uploads content.
        /// </summary>
        /// <param name="media">Die Media.</param>
        /// <param name="data">The Data.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async Task<string> CreateAsync(TEntity media, byte[] data, string mimeType, string fileName, string fileExtension, CancellationToken cancellationToken = default)
        {
            var id = await this.Create(media, cancellationToken).ConfigureAwait(false);
            await this.UploadAsync(id, data, mimeType, fileExtension, fileName, cancellationToken).ConfigureAwait(false);
            return id;
        }

        /// <summary>
        /// Uploads any binary Data to a Media Entity.
        /// </summary>
        /// <param name="id">Die Entity ID.</param>
        /// <param name="data">The data.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task UploadAsync(string id, byte[] data, string mimeType, string fileName, string fileExtension, CancellationToken cancellationToken = default)
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
            if(parameters.Count > 0)
            {
                uri += "?" + string.Join("&", parameters);
            }

            await this.Client.PostBytes(uri, data, null, null, fileExtension, mimeType, cancellationToken);
        }

        /// <summary>
        /// Rename a file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task RenameAsync(string id, string fileName, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(fileName, nameof(fileName));
            var uri = $"/_action/{this.EntityRoute}/{id}/rename";
            await this.Client.Post(uri, new Dictionary<string, string> { { "fileName", fileName }}, cancellationToken);
        }
    }

    public class MediaRepository : MediaRepository<Media>
    {
        public MediaRepository(Client client) : base(client)
        {
        }
    }
}
