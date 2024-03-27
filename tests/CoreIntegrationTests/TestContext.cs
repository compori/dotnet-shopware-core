using Compori.Shopware.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware
{
    public class TestContext
    {
        private Client client = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestContext"/> class.
        /// </summary>
        public TestContext()
        {
        }

        /// <summary>
        /// Creates the settings factory.
        /// </summary>
        /// <returns>ISettingsFactory.</returns>
        public ISettingsFactory CreateSettingsFactory(string configFile)
        {
            var factory = new SettingsFactory();
            factory.ReadFromJsonFile(configFile);
            return factory;
        }

        /// <summary>
        /// Creates the settings factory.
        /// </summary>
        /// <returns>ISettingsFactory.</returns>
        public ISettingsFactory CreateSettingsFactory()
        {
            return this.CreateSettingsFactory("testing-shop.ignore.json");
        }

        /// <summary>
        /// Liefert die Testeinstellungen zurück.
        /// </summary>
        /// <returns>Settings.</returns>
        public Settings GetSettings()
        {
            var factory = this.CreateSettingsFactory();
            return factory.Create();
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="configFile">The configuration file.</param>
        /// <returns>Client.</returns>
        public Client CreateClient(string configFile)
        {
            var settingsFactory = this.CreateSettingsFactory(configFile);
            var restClientFactory = new RestClientFactory();
            return new Client(settingsFactory, restClientFactory);
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <returns>Client.</returns>
        public Client CreateClient()
        {
            if (this.client == null)
            {
                var settingsFactory = this.CreateSettingsFactory();
                var restClientFactory = new RestClientFactory();
                this.client = new Client(settingsFactory, restClientFactory);
            }
            return this.client;
        }

        /// <summary>
        /// Creates the country repository.
        /// </summary>
        /// <returns>Repositories.CountryRepository.</returns>
        public Repositories.CountryRepository CreateCountryRepository()
        {
            var client = this.CreateClient();
            return new Repositories.CountryRepository(client);
        }

        /// <summary>
        /// Creates the country state repository.
        /// </summary>
        /// <returns>Repositories.CountryStateRepository.</returns>
        public Repositories.CountryStateRepository CreateCountryStateRepository()
        {
            var client = this.CreateClient();
            return new Repositories.CountryStateRepository(client);
        }

        /// <summary>
        /// Creates the currency repository.
        /// </summary>
        /// <returns>Repositories.CurrencyRepository.</returns>
        public Repositories.CurrencyRepository CreateCurrencyRepository()
        {
            var client = this.CreateClient();
            return new Repositories.CurrencyRepository(client);
        }

        /// <summary>
        /// Creates the customer repository.
        /// </summary>
        /// <returns>Repositories.CustomerRepository.</returns>
        public Repositories.CustomerRepository CreateCustomerRepository()
        {
            var client = this.CreateClient();
            return new Repositories.CustomerRepository(client);
        }


        /// <summary>
        /// Creates the customer group repository.
        /// </summary>
        /// <returns>Repositories.CustomerGroupRepository.</returns>
        public Repositories.CustomerGroupRepository CreateCustomerGroupRepository()
        {
            var client = this.CreateClient();
            return new Repositories.CustomerGroupRepository(client);
        }

        /// <summary>
        /// Creates the order repository.
        /// </summary>
        /// <returns>Repositories.OrderRepository.</returns>
        public Repositories.OrderRepository CreateOrderRepository()
        {
            var client = this.CreateClient();
            return new Repositories.OrderRepository(client);
        }

        /// <summary>
        /// Creates the order line item repository.
        /// </summary>
        /// <returns>Repositories.OrderLineItemRepository.</returns>
        public Repositories.OrderLineItemRepository CreateOrderLineItemRepository()
        {
            var client = this.CreateClient();
            return new Repositories.OrderLineItemRepository(client);
        }

        /// <summary>
        /// Creates the payment method repository.
        /// </summary>
        /// <returns>Repositories.PaymentMethodRepository.</returns>
        public Repositories.PaymentMethodRepository CreatePaymentMethodRepository()
        {
            var client = this.CreateClient();
            return new Repositories.PaymentMethodRepository(client);
        }

        /// <summary>
        /// Creates the state machine repository.
        /// </summary>
        /// <returns>Repositories.StateMachineRepository.</returns>
        public Repositories.StateMachineRepository CreateStateMachineRepository()
        {
            var client = this.CreateClient();
            return new Repositories.StateMachineRepository(client);
        }

        /// <summary>
        /// Creates the state machine state repository.
        /// </summary>
        /// <returns>Repositories.StateMachineStateRepository.</returns>
        public Repositories.StateMachineStateRepository CreateStateMachineStateRepository()
        {
            var client = this.CreateClient();
            return new Repositories.StateMachineStateRepository(client);
        }

        /// <summary>
        /// Creates the order reader.
        /// </summary>
        /// <returns>Helpers.OrderReader.</returns>
        public Helpers.OrderReader CreateOrderReader()
        {
            return new Helpers.OrderReader(
                this.CreateCountryRepository(),
                this.CreateCountryStateRepository(),
                this.CreateCurrencyRepository(),
                this.CreateCustomerRepository(),
                this.CreateCustomerGroupRepository(),
                this.CreateOrderRepository(),
                this.CreateOrderLineItemRepository(),
                this.CreatePaymentMethodRepository(),
                this.CreateStateMachineRepository(),
                this.CreateStateMachineStateRepository(),
                this.CreateShippingMethodRepository()
            );
        }

        /// <summary>
        /// Creates the product repository.
        /// </summary>
        /// <returns>Repositories.ProductRepository.</returns>
        public Repositories.ProductRepository CreateProductRepository()
        {
            var client = this.CreateClient();
            return new Repositories.ProductRepository(client);
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <returns>Entities.Product.</returns>
        public async Task<Entities.Product> CreateProduct()
        {
            var client = this.CreateClient();
            var repository = new Repositories.ProductRepository(client);
            var currencyRepository = new Repositories.CurrencyRepository(client);
            var currencies = await currencyRepository.ReadAll();
            var currency = currencies.FirstOrDefault(v => v.Value.IsSystemDefault ?? false).Value;
            var taxRepository = new Repositories.TaxRepository(client);

            var taxes = await taxRepository.ReadAll();
            var tax = taxes.FirstOrDefault().Value;

            var productNumber = Guid.NewGuid().ToString();
            var productName = "Product " + Guid.NewGuid().ToString();
            var product = new Entities.Product
            {
                ProductNumber = productNumber,
                Name = productName,
                Stock = 0,
                Price = new List<Types.Price>()
                {
                    new Types.Price
                    {
                        CurrencyId = currency.Id,
                        Gross = 1 * (1 + (tax.TaxRate / 100)),
                        Net = 1,
                        Linked = true
                    }
                },
                TaxId = tax.Id,
            };
            var id = await repository.Create(product);
            product.Id = id;

            return product;
        }

        /// <summary>
        /// Creates the media repository.
        /// </summary>
        /// <returns>Repositories.MediaRepository.</returns>
        public Repositories.MediaRepository CreateMediaRepository()
        {
            var client = this.CreateClient();
            return new Repositories.MediaRepository(client);
        }

        /// <summary>
        /// Creates the product repository.
        /// </summary>
        /// <returns>Repositories.ShippingMethodRepository.</returns>
        public Repositories.ShippingMethodRepository CreateShippingMethodRepository()
        {
            var client = this.CreateClient();
            return new Repositories.ShippingMethodRepository(client);
        }

        /// <summary>
        /// Creates the property group repository.
        /// </summary>
        /// <returns>Repositories.PropertyGroupRepository.</returns>
        public Repositories.PropertyGroupRepository CreatePropertyGroupRepository()
        {
            var client = this.CreateClient();
            return new Repositories.PropertyGroupRepository(client);
        }

        /// <summary>
        /// Creates the property group option repository.
        /// </summary>
        /// <returns>Repositories.PropertyGroupOptionRepository.</returns>
        public Repositories.PropertyGroupOptionRepository CreatePropertyGroupOptionRepository()
        {
            var client = this.CreateClient();
            return new Repositories.PropertyGroupOptionRepository(client);
        }

        /// <summary>
        /// Creates the tag repository.
        /// </summary>
        /// <returns>Repositories.TagRepository.</returns>
        public Repositories.TagRepository CreateTagRepository()
        {
            var client = this.CreateClient();
            return new Repositories.TagRepository(client);
        }

        /// <summary>
        /// Creates the property group.
        /// </summary>
        /// <returns>Entities.PropertyGroup.</returns>
        public async Task<Entities.PropertyGroup> CreatePropertyGroup()
        {
            var repository = this.CreatePropertyGroupRepository();

            var name = "PropGroup " + Guid.NewGuid().ToString();
            var group = new Entities.PropertyGroup
            {
                Name = name,
                DisplayType = "select",
                SortingType = "alphanumeric",
                Filterable = false,
                VisibleOnProductDetailPage = false,
            };
            var id = await repository.Create(group).ConfigureAwait(false);
            Assert.NotNull(id);
            group.Id = id;
            return group;
        }

        /// <summary>
        /// Removes the property group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeletePropertyGroup(string id)
        {
            var repository = this.CreatePropertyGroupRepository();
            await repository.Delete(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the property group option.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>Entities.PropertyGroupOption.</returns>
        public async Task<Entities.PropertyGroupOption> CreatePropertyGroupOption(string groupId)
        {
            var repository = this.CreatePropertyGroupOptionRepository();

            var name = "PropGroupOption " + Guid.NewGuid().ToString();
            var option = new Entities.PropertyGroupOption
            {
                Name = name,
                GroupId = groupId
            };

            var id = await repository.Create(option).ConfigureAwait(false);
            Assert.NotNull(id);
            option.Id = id;
            return option;
        }

        /// <summary>
        /// Removes the property group option.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeletePropertyGroupOption(string id)
        {
            var repository = this.CreatePropertyGroupOptionRepository();
            await repository.Delete(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Create tag as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;Entities.Tag&gt; representing the asynchronous operation.</returns>
        public async Task<Entities.Tag> CreateTag()
        {
            var name = "Test Tag " + Guid.NewGuid().ToString();
            var tag = new Entities.Tag
            {
                Name = name,
            };
            var repository = this.CreateTagRepository();
            var id = await repository.Create(tag).ConfigureAwait(false);
            return await repository.Read(id);
        }

        /// <summary>
        /// Deletes a tag as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeleteTag(string id)
        {
            var repository = this.CreateTagRepository();
            await repository.Delete(id).ConfigureAwait(false);
        }

        public async Task DeleteMedia(string id)
        {
            var repository = this.CreateMediaRepository();
            await repository.Delete(id).ConfigureAwait(false);
        }

        public async Task<Entities.Media> CreateMedia()
        {
            var repository = this.CreateMediaRepository();

            var fileName = "test-image.jpg";
            var fileInfo = new FileInfo(Path.Combine("data", fileName));
            var mimeType = Helpers.MimeTypeHelper.GetMimeTypeByFileName(fileName);
            var id = await repository.Create(new Entities.Media
            {
                Title = "My Plants",
                Alt = "Nice Pictures of Plants",
                MediaFolderId = "bd98aa186730462db38628427835dacb", // Test Folder
            });
            var extension = fileInfo.Extension.TrimStart('.');
            await repository.Upload(id, File.ReadAllBytes(fileInfo.FullName), Helpers.MimeTypeHelper.GetMimeTypeByExtension(extension), "test picture", extension);
            var media = await repository.Read(id);
            return media;
        }
    }
}
