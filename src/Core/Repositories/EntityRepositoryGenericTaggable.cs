using Compori.Shopware.Attributes;
using Compori.Shopware.Entities;
using Compori.Shopware.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Repositories
{
    public class EntityRepository<TEntity, TTaggableWith, TTag> : EntityRepository<TEntity>
        where TEntity : Entity, ITaggable<TTaggableWith>
        where TTaggableWith : ITaggableWith, new()
        where TTag : Tag
    {
        /// <summary>
        /// Gets the name of the taggable entity identifier field.
        /// </summary>
        /// <value>The name of the taggable entity identifier field.</value>
        protected string TaggableEntityIdFieldName { get; }

        /// <summary>
        /// Gets the name of the tag entity identifier field.
        /// </summary>
        /// <value>The name of the tag entity identifier field.</value>
        protected string TaggableTagIdFieldName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{TEntity, TTaggableWith, TTag}"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public EntityRepository(Client client) : base(client)
        {
            this.TaggableEntityIdFieldName = ShopwareEntityAttribute.GetTaggableEntityIdFieldName<TTaggableWith>();
            this.TaggableTagIdFieldName = ShopwareEntityAttribute.GetTaggableTagIdFieldName<TTaggableWith>();
        }

        /// <summary>
        /// Reads all tags for an entity.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List&lt;TTag&gt;.</returns>
        public async Task<List<TTag>> ReadTags(string id, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            return await this.ReadLinkedEntities<TTag>(id, "tags", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replaces the tags.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="tagIds">The tag ids.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List&lt;TTag&gt;.</returns>
        public async Task<List<TTag>> ReplaceTags(string id, List<string> tagIds, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNull(tagIds, nameof(tagIds));

            var tags = await this.ReadTags(id, cancellationToken).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested)
            {
                return tags;
            }

            // Remove tags which are not in tagIds paramter
            if (tags != null && tags.Count > 0)
            {
                var existingTagIds = tags.Select(v => v.Id);
                var removingTagIds = existingTagIds.Except(tagIds).ToList();
                var remainingTagIds = existingTagIds.Intersect(tagIds).ToList();

                if (removingTagIds.Count > 0)
                {
                    await this.RemoveTags(id, removingTagIds, cancellationToken).ConfigureAwait(false);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return tags.Where(v => remainingTagIds.Contains(v.Id)).ToList();
                }
                tagIds = tagIds.Except(remainingTagIds).ToList();
            }

            // Upsert all new tags assignments.
            if (tagIds.Count > 0)
            {
                await this.AddTags(id, tagIds, cancellationToken).ConfigureAwait(false);
                tags = await this.ReadTags(id, cancellationToken).ConfigureAwait(false);
            }

            return tags;
        }

        /// <summary>
        /// Adds a tag.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task AddTag(string id, string tagId, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(tagId, nameof(tagId));
            await this.AddTags(id, new List<string> { tagId }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task AddTags(string id, List<string> tagIds, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNull(tagIds, nameof(tagIds));

            if (tagIds.Count <= 0)
            {
                return;
            }

            // Upsert all new tags.
            var taggableWithEntities = tagIds.Select(v => new TTaggableWith { EntityId = id, TagId = v }).ToList();
            await this.Sync(new BulkUpsert<TTaggableWith>(taggableWithEntities), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds tags to entities.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task AddTags(List<TTaggableWith> tags, CancellationToken cancellationToken = default)
        {
            if (tags.Count <= 0)
            {
                return;
            }

            // Upsert the tags.
            await this.Sync(new BulkUpsert<TTaggableWith>(tags), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Removes the the given tag ids from entity.
        /// </summary>
        /// <param name="id">The entitiy identifier.</param>
        /// <param name="tagIds">The tag id.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task RemoveTags(string id, List<string> tagIds, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(tagIds, nameof(tagIds));

            if (tagIds.Count <= 0)
            {
                return;
            }

            await this.Sync(
                new BulkDelete<TTaggableWith>(
                    this.TaggableEntityIdFieldName,
                    this.TaggableTagIdFieldName,
                    new Dictionary<string, List<string>> { { id, tagIds } }), cancellationToken).ConfigureAwait(false);
        }
    }
}
