using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class ProductRepositoryTests : BaseTest
    {
        protected ProductRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = this.TestContext.CreateProductRepository();
        }

        [Fact()]
        public async Task TestCreate()
        {
            this.Setup();
            Entities.Product product = null;
            var repository = this.Repository;
            try
            {
                product = await this.TestContext.CreateProduct();

                //
                var actual = await repository.Read(product.Id);
                Assert.NotNull(actual);
                Assert.Equal(product.Id, actual.Id);
                Assert.Equal(product.Name, actual.Name);
                Assert.Equal(product.ProductNumber, actual.ProductNumber);
            }
            finally
            {
                if (product != null)
                {
                    await repository.Delete(product.Id);
                }
            }
        }

        [Fact()]
        public async Task TestReadAndReplacePropertyOptions()
        {
            this.Setup();
            Entities.Product product = null;
            Entities.PropertyGroup group = null;
            Entities.PropertyGroupOption option = null;
            try
            {
                product = await this.TestContext.CreateProduct();
                var productId = product.Id;

                var options = await this.Repository.ReadPropertyOptions(productId).ConfigureAwait(false);
                Assert.NotNull(options);
                Assert.Empty(options);

                group = await this.TestContext.CreatePropertyGroup();
                option = await this.TestContext.CreatePropertyGroupOption(group.Id);

                await this.Repository.ReplacePropertyOptions(productId, new List<string> { option.Id }).ConfigureAwait(false);

                options = await this.Repository.ReadPropertyOptions(productId).ConfigureAwait(false);
                Assert.NotNull(options);
                Assert.Single(options);
                Assert.Equal(option.Id, options[0].Id);

                await this.Repository.ReplacePropertyOptions(productId, new List<string> { }).ConfigureAwait(false);

                options = await this.Repository.ReadPropertyOptions(productId).ConfigureAwait(false);
                Assert.NotNull(options);
                Assert.Empty(options);
            }
            finally
            {
                if (product != null)
                {
                    await this.Repository.Delete(product.Id);
                }
                if (option != null)
                {
                    await this.TestContext.DeletePropertyGroupOption(option.Id);
                }
                if (group != null)
                {
                    await this.TestContext.DeletePropertyGroup(group.Id);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestConfigurationSettingsWithOptions()
        {
            this.Setup();
            Entities.Product parentProduct = null;
            Entities.Product child1Product = null;
            Entities.Product child2Product = null;
            Entities.PropertyGroup group = null;
            Entities.PropertyGroupOption option1 = null;
            Entities.PropertyGroupOption option2 = null;
            try
            {
                parentProduct = await this.TestContext.CreateProduct();

                group = await this.TestContext.CreatePropertyGroup();
                option1 = await this.TestContext.CreatePropertyGroupOption(group.Id);
                option2 = await this.TestContext.CreatePropertyGroupOption(group.Id);

                // Set parent product configuration
                var settings = await this.Repository.ReadConfiguratorSettings(parentProduct.Id).ConfigureAwait(false);
                var newSettings = new List<Entities.ProductConfiguratorSetting>(settings)
                {
                    new Entities.ProductConfiguratorSetting { OptionId = option1.Id },
                    new Entities.ProductConfiguratorSetting { OptionId = option2.Id }
                };
                await this.Repository.ReplaceConfiguratorSettings(parentProduct.Id, newSettings);

                // first child
                child1Product = await this.TestContext.CreateProduct();
                var child1Options = await this.Repository.ReadConfigurationOptions(child1Product.Id).ConfigureAwait(false);
                Assert.NotNull(child1Options);
                Assert.Empty(child1Options);
                var newChild1Options = new List<Entities.PropertyGroupOption>(child1Options)
                {
                    option1
                };
                await this.Repository.ReplaceConfigurationOptions(child1Product.Id, newChild1Options.Select(v => v.Id).ToList()).ConfigureAwait(false);
                child1Product.ParentId = parentProduct.Id;
                await this.Repository.Update(child1Product);

                child1Options = await this.Repository.ReadConfigurationOptions(child1Product.Id).ConfigureAwait(false);
                Assert.NotNull(child1Options);
                Assert.Equal(newChild1Options.Count, child1Options.Count);
                Assert.Contains(option1.Id, child1Options.Select(v => v.Id));

                // second child
                child2Product = await this.TestContext.CreateProduct();
                var child2Options = await this.Repository.ReadConfigurationOptions(child2Product.Id).ConfigureAwait(false);
                Assert.NotNull(child2Options);
                Assert.Empty(child2Options);
                var newChild2Options = new List<Entities.PropertyGroupOption>(child2Options)
                {
                    option2
                };
                await this.Repository.ReplaceConfigurationOptions(child2Product.Id, newChild2Options.Select(v => v.Id).ToList()).ConfigureAwait(false);
                child2Product.ParentId = parentProduct.Id;
                await this.Repository.Update(child2Product);

                child2Options = await this.Repository.ReadConfigurationOptions(child2Product.Id).ConfigureAwait(false);
                Assert.NotNull(child2Options);
                Assert.Equal(newChild2Options.Count, child2Options.Count);
                Assert.Contains(option2.Id, child2Options.Select(v => v.Id));


                // Read setting parent
                var actualSettings = await this.Repository.ReadConfiguratorSettings(parentProduct.Id).ConfigureAwait(false);
                Assert.NotNull(actualSettings);
                Assert.Equal(2, actualSettings.Count);
                Assert.Contains(option1.Id, actualSettings.Select(v => v.OptionId));
                Assert.Contains(option2.Id, actualSettings.Select(v => v.OptionId));

                var parentOptions = await this.Repository.ReadConfigurationOptions(parentProduct.Id).ConfigureAwait(false);
                Assert.NotNull(parentOptions);
                Assert.Empty(parentOptions);
            }
            finally
            {
                if (child2Product != null)
                {
                    await this.Repository.Delete(child2Product.Id);
                }
                if (child1Product != null)
                {
                    await this.Repository.Delete(child1Product.Id);
                }
                if (parentProduct != null)
                {
                    await this.Repository.Delete(parentProduct.Id);
                }
                if (option1 != null)
                {
                    await this.TestContext.DeletePropertyGroupOption(option1.Id);
                }
                if (option2 != null)
                {
                    await this.TestContext.DeletePropertyGroupOption(option2.Id);
                }
                if (group != null)
                {
                    await this.TestContext.DeletePropertyGroup(group.Id);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestReadAndReplaceCategories()
        {
            this.Setup();
            Entities.Product product = null;
            var categoryRepository = new CategoryRepository(this.TestContext.CreateClient());
            try
            {
                product = await this.TestContext.CreateProduct();
                var categories = await categoryRepository.ReadAll(new Types.Search
                {
                    Limit = 100,
                    Filters = new List<Types.Filter>
                    {
                        new Types.Filter
                        {
                            Field = "name",
                            Type = "equals",
                            Value = "Home"
                        }
                    }.ToArray()
                }).ConfigureAwait(false);
                Assert.NotNull(categories);
                var homeCategory = categories.FirstOrDefault().Value;
                Assert.NotNull(homeCategory);
                //
                var actual = await this.Repository.Read(product.Id);
                Assert.NotNull(actual);
                Assert.Equal(product.Id, actual.Id);
                Assert.Equal(product.Name, actual.Name);
                Assert.Equal(product.ProductNumber, actual.ProductNumber);

                var items = await this.Repository.ReadCategories(product.Id).ConfigureAwait(false);
                Assert.NotNull(items);
                Assert.Empty(items);
                await this.Repository.ReplaceCategories(product.Id, new List<string> { homeCategory.Id });
                items = await this.Repository.ReadCategories(product.Id).ConfigureAwait(false);
                Assert.NotNull(items);
                Assert.Single(items);
                Assert.Contains(homeCategory.Id, items.Select(v => v.Id));

                await this.Repository.ReplaceCategories(product.Id, new List<string> { });
                items = await this.Repository.ReadCategories(product.Id).ConfigureAwait(false);
                Assert.NotNull(items);
                Assert.Empty(items);
            }
            finally
            {
                if (product != null)
                {
                    await this.Repository.Delete(product.Id);
                }
                this.Cleanup();
            }
        }



        [Fact()]
        public async Task TestProductMediaAsync()
        {
            this.Setup();
            Entities.Product product = null;
            Entities.Media media = null;
            try
            {
                product = await this.TestContext.CreateProduct();
                media = await this.TestContext.CreateMedia();
                var items = await this.Repository.ReadMedia(product.Id).ConfigureAwait(false);
                Assert.NotNull(items);
                Assert.Empty(items);
                await this.Repository.ReplaceMedia(product.Id, new List<Entities.ProductMedia>
                {
                    new Entities.ProductMedia
                    {
                        ProductId = product.Id,
                        MediaId = media.Id
                    }
                });
                items = await this.Repository.ReadMedia(product.Id).ConfigureAwait(false);
                Assert.NotNull(items);
                Assert.Single(items);
                Assert.Contains(media.Id, items.Select(v => v.MediaId));

                await this.Repository.ReplaceMedia(product.Id, new List<Entities.ProductMedia> { }).ConfigureAwait(false);
                items = await this.Repository.ReadMedia(product.Id).ConfigureAwait(false);
                Assert.NotNull(items);
                Assert.Empty(items);
            }
            finally
            {
                if (product != null)
                {
                    await this.Repository.Delete(product.Id);
                }
                if (media != null)
                {
                    await this.TestContext.DeleteMedia(media.Id);
                }
                this.Cleanup();
            }
        }
    }
}