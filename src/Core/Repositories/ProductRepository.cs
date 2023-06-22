using Compori.Shopware.Types;
using Compori.Shopware.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Compori.Shopware.Repositories
{
    public class ProductRepository<TEntity, TPrice, TProductVisibility, TProductManufacturer, TUnit, TTaggableWith, TTag>
        : EntityRepository<TEntity, TTaggableWith, TTag>
        where TEntity : Product<TPrice, TProductVisibility, TProductManufacturer, TUnit, TTaggableWith>
        where TTaggableWith : ProductTag, new()
        where TTag : Tag
        where TPrice : Price
        where TProductVisibility : ProductVisibility
        where TProductManufacturer : ProductManufacturer
        where TUnit : Unit
    {
        /// <param name="client">The client.</param>
        public ProductRepository(Client client) : base(client)
        {
        }

        #region Media

        /// <summary>
        /// Read all product media entities as an asynchronous operation.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<ProductMedia>> ReadMedia(string id, CancellationToken cancellationToken = default)
        {
            return await this.ReadMedia<ProductMedia>(id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Read all product media entities as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TProductMedia">The product media type.</typeparam>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<TProductMedia>> ReadMedia<TProductMedia>(
            string id, CancellationToken cancellationToken = default
        ) where TProductMedia : ProductMedia
        {
            return await this.ReadLinkedEntities<TProductMedia>(id, "media", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replace product media entities as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReplaceMedia(string id, List<ProductMedia> entities, CancellationToken cancellationToken = default)
        {
            await this.ReplaceMedia<ProductMedia>(id, entities, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replace product media entities as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TProductMedia">The type of the t product media.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="media">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReplaceMedia<TProductMedia>(
                    string id, List<TProductMedia> media, CancellationToken cancellationToken = default
                ) where TProductMedia : ProductMedia
        {
            var existingMedia = await this.ReadMedia<TProductMedia>(id, cancellationToken).ConfigureAwait(false);

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (existingMedia != null && existingMedia.Count > 0)
            {
                // Remove product media entities that not in list parameter.
                var existingMediaIds = existingMedia.Select(v => v.MediaId);
                var removingMediaIds = existingMediaIds.Except(media.Select(v => v.MediaId));
                var removingProductMediaIds = existingMedia.Where(v => removingMediaIds.Contains(v.MediaId)).Select(v => v.Id).ToList();
                if (removingProductMediaIds.Count > 0)
                {
                    await this.Sync(new BulkDelete<TProductMedia>(removingProductMediaIds), cancellationToken).ConfigureAwait(false);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            // Upsert entities.
            if (media.Count > 0)
            {
                foreach(var item in media)
                {
                    item.ProductId = id;
                }
                await this.Sync(new BulkUpsert<TProductMedia>(media), cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region Configuration Settings

        /// <summary>
        /// Read all product configurator settings as an asynchronous operation.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<ProductConfiguratorSetting>> ReadConfiguratorSettings(
            string id, CancellationToken cancellationToken = default
        )
        {
            return await this.ReadConfiguratorSettings<ProductConfiguratorSetting>(id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Read all product configurator settings as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TProductConfiguratorSetting">The prduct configuration setting type.</typeparam>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<TProductConfiguratorSetting>> ReadConfiguratorSettings<TProductConfiguratorSetting>(
            string id, CancellationToken cancellationToken = default
        ) where TProductConfiguratorSetting : ProductConfiguratorSetting
        {
            return await this.ReadLinkedEntities<TProductConfiguratorSetting>(id, "configuratorSettings", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replace configurator settings as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReplaceConfiguratorSettings(string id, List<ProductConfiguratorSetting> entities, CancellationToken cancellationToken = default)
        {
            await this.ReplaceConfiguratorSettings<ProductConfiguratorSetting>(id, entities, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replace product media entities as an asynchronous operation.
        /// This method causes min one and max three remote calls.
        /// </summary>
        /// <typeparam name="TProductConfiguratorSetting">The type of the t product media.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="settings">The entities.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReplaceConfiguratorSettings<TProductConfiguratorSetting>(
                    string id, List<TProductConfiguratorSetting> settings, CancellationToken cancellationToken = default
                ) where TProductConfiguratorSetting : ProductConfiguratorSetting
        {
            // Read existing configurator settings
            var existingSettings = await this.ReadConfiguratorSettings<TProductConfiguratorSetting>(id, cancellationToken).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (existingSettings != null && existingSettings.Count > 0)
            {
                // Remove configurator setting which property option is not in settings parameter.
                var existingOptionIds = existingSettings.Select(v => v.OptionId);
                var removingOptionIds = existingOptionIds.Except(settings.Select(v => v.OptionId));
                var removingSettingsIds = existingSettings.Where(v => removingOptionIds.Contains(v.OptionId)).Select(v => v.Id).ToList();
                if (removingSettingsIds.Count > 0)
                {
                    await this.Sync(new BulkDelete<TProductConfiguratorSetting>(removingSettingsIds), cancellationToken).ConfigureAwait(false);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            // Upsert all new settings.
            if (settings.Count > 0)
            {
                foreach(var setting in settings)
                {
                    setting.ProductId = id;
                }
                await this.Sync(new BulkUpsert<TProductConfiguratorSetting>(settings), cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region Configuration Options

        /// <summary>
        /// Read all product configuration options entities.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<PropertyGroupOption>> ReadConfigurationOptions(string id, CancellationToken cancellationToken = default)
        {
            return await this.ReadConfigurationOptions<PropertyGroupOption>(id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Read all product configuration options entities.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<TPropertyGroupOption>> ReadConfigurationOptions<TPropertyGroupOption>(
            string id, CancellationToken cancellationToken = default
        ) where TPropertyGroupOption : PropertyGroupOption
        {
            return await this.ReadLinkedEntities<TPropertyGroupOption>(id, "options", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replace all configuration options.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="optionIds">The option ids.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReplaceConfigurationOptions(string id, List<string> optionIds, CancellationToken cancellationToken = default)
        {
            // var existingOptionIds = await this.ReadProperty<List<string>>(id, "optionIds", cancellationToken).ConfigureAwait(false);
            // Returns Error:
            // Attempted to call an undefined method named "getReferenceDefinition" of class "Shopware\\Core\\Framework\\DataAbstractionLayer\\Field\\ManyToManyIdField".
            var existingOptions = await this.ReadConfigurationOptions(id, cancellationToken).ConfigureAwait(false);
            var existingOptionIds = existingOptions.Select(existing => existing.Id).Distinct().ToList();
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (existingOptionIds != null && existingOptionIds.Count > 0)
            {
                // Remove option ids which are not in optionIds parameter.
                var removingOptionIds = existingOptionIds.Except(optionIds).ToList();
                if (removingOptionIds.Count > 0)
                {
                    await this.Sync(
                        new BulkDelete<ProductOption>(
                            "productId",
                            "optionId",
                            new Dictionary<string, List<string>> { { id, removingOptionIds } }), cancellationToken).ConfigureAwait(false);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            // Upsert all new optionIds.
            if (optionIds.Count > 0)
            {
                await this.Sync(
                        new BulkUpsert<ProductOption>(
                        optionIds.Select(v => new ProductOption
                        {
                            ProductId = id,
                            OptionId = v
                        }).ToList()),
                        cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        #endregion

        #region Property Options

        /// <summary>
        /// Read all product property options entities.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<PropertyGroupOption>> ReadPropertyOptions(string id, CancellationToken cancellationToken = default)
        {
            return await this.ReadPropertyOptions<PropertyGroupOption>(id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Read all product property options entities.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<TPropertyGroupOption>> ReadPropertyOptions<TPropertyGroupOption>(
            string id, CancellationToken cancellationToken = default
        ) where TPropertyGroupOption : PropertyGroupOption
        {
            return await this.ReadLinkedEntities<TPropertyGroupOption>(id, "properties", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replaces the product property options.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="optionIds">The option ids.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task ReplacePropertyOptions(string id, List<string> optionIds, CancellationToken cancellationToken = default)
        {
            // var existingPropertyIds = await this.ReadProperty<List<string>>(id, "propertyIds", cancellationToken).ConfigureAwait(false);
            // Returns Error:
            // Attempted to call an undefined method named "getReferenceDefinition" of class "Shopware\\Core\\Framework\\DataAbstractionLayer\\Field\\ManyToManyIdField".
            var existingProperties = await this.ReadPropertyOptions(id, cancellationToken).ConfigureAwait(false);
            var existingPropertyIds = existingProperties.Select(existing => existing.Id).Distinct().ToList();
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (existingPropertyIds != null && existingPropertyIds.Count > 0)
            {
                // Remove configurator setting which property option is not in settings parameter.
                var removingPropertyIds = existingPropertyIds.Except(optionIds).ToList();
                if (removingPropertyIds.Count > 0)
                {
                    await this.Sync(
                        new BulkDelete<ProductProperty>(
                            "productId",
                            "optionId",
                            new Dictionary<string, List<string>> { { id, removingPropertyIds } }), cancellationToken).ConfigureAwait(false);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            // Upsert all new properties.
            if (optionIds.Count > 0)
            {
                await this.Sync(
                        new BulkUpsert<ProductProperty>(
                        optionIds.Select(v => new ProductProperty
                        {
                            ProductId = id,
                            OptionId = v
                        }).ToList()),
                        cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        #endregion

        #region Categories

        /// <summary>
        /// Read all product categories entities.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<Category>> ReadCategories(string id, CancellationToken cancellationToken = default)
        {
            return await this.ReadCategories<Category>(id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Read all product property options entities.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<TCategory>> ReadCategories<TCategory>(
            string id, CancellationToken cancellationToken = default
        ) where TCategory : Category
        {
            return await this.ReadLinkedEntities<TCategory>(id, "categories", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Replaces the categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="categoryIds">The category ids.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task ReplaceCategories(string id, List<string> categoryIds, CancellationToken cancellationToken = default)
        {
            var existingCategories = await this.ReadCategories(id, cancellationToken).ConfigureAwait(false);
            var existingCategoryIds = existingCategories.Select(v => v.Id).Distinct().ToList();   
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (existingCategoryIds != null && existingCategoryIds.Count > 0)
            {
                // Remove category which id is not in categoryIds parameter.
                var removingCategoryIds = existingCategoryIds.Except(categoryIds).ToList();
                if (removingCategoryIds.Count > 0)
                {
                    await this.Sync(
                        new BulkDelete<ProductCategory>(
                            "productId",
                            "categoryId",
                            new Dictionary<string, List<string>> { { id, removingCategoryIds } }), cancellationToken).ConfigureAwait(false);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            // Upsert all new category assignments.
            if (categoryIds.Count > 0)
            {
                await this.Sync(
                        new BulkUpsert<ProductCategory>(
                        categoryIds.Select(v => new ProductCategory
                        {
                            ProductId = id,
                            CategoryId = v
                        }).ToList()),
                        cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        #endregion
    }

    public class ProductRepository : ProductRepository<Product, Price, ProductVisibility, ProductManufacturer, Unit, ProductTag, Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="bulkService">The bulk service.</param>
        public ProductRepository(Client client) : base(client)
        {
        }
    }
}