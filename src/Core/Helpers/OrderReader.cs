using Compori.Shopware.Entities;
using Compori.Shopware.Repositories;
using Compori.Shopware.Types;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compori.Shopware.Helpers
{
    public class OrderReader : OrderReader<Country, CountryState, Currency, Customer, CustomerGroup, Order, OrderLineItem, PaymentMethod, StateMachine, StateMachineState>
    {
        public OrderReader(
            CountryRepository countryRepository,
            CountryStateRepository countryStateRepository,
            CurrencyRepository currencyRepository,
            CustomerRepository customerRepository,
            CustomerGroupRepository customerGroupRepository,
            OrderRepository orderRepository,
            OrderLineItemRepository orderLineItemRepository,
            PaymentMethodRepository paymentMethodRepository,
            StateMachineRepository stateMachineRepository,
            StateMachineStateRepository stateMachineStateRepository,
            ShippingMethodRepository shippingMethodRepository
        ) : base(
            countryRepository,
            countryStateRepository,
            currencyRepository,
            customerRepository,
            customerGroupRepository,
            orderRepository,
            orderLineItemRepository,
            paymentMethodRepository,
            stateMachineRepository,
            stateMachineStateRepository,
            shippingMethodRepository)
        {
        }
    }

    public class OrderReader<TCountry, TCountryState, TCurrency, TCustomer, TCustomerGroup, TOrder, TOrderLineItem, TPaymentMethod, TStateMachine, TStateMachineState>
        where TCountry : Country
        where TCountryState : CountryState
        where TCurrency : Currency
        where TCustomer : Customer
        where TCustomerGroup : CustomerGroup
        where TOrder : Order
        where TOrderLineItem : OrderLineItem
        where TPaymentMethod : PaymentMethod
        where TStateMachine : StateMachine
        where TStateMachineState : StateMachineState
    {
        /// <summary>
        /// Gets the country repository.
        /// </summary>
        /// <value>The country repository.</value>
        private CountryRepository<TCountry> CountryRepository { get; }

        /// <summary>
        /// Gets the country state repository.
        /// </summary>
        /// <value>The country state repository.</value>
        private CountryStateRepository<TCountryState> CountryStateRepository { get; }

        /// <summary>
        /// Sets the currency repository.
        /// </summary>
        /// <value>The currency repository.</value>
        private CurrencyRepository<TCurrency> CurrencyRepository { get; }

        /// <summary>
        /// Gets the customer repository.
        /// </summary>
        /// <value>The customer repository.</value>
        private CustomerRepository<TCustomer> CustomerRepository { get; }

        /// <summary>
        /// Gets the customer group repository.
        /// </summary>
        /// <value>The customer group repository.</value>
        private CustomerGroupRepository<TCustomerGroup> CustomerGroupRepository { get; }

        /// <summary>
        /// Gets the order repository.
        /// </summary>
        /// <value>The order repository.</value>
        private OrderRepository<TOrder> OrderRepository { get; }

        /// <summary>
        /// Gets the order line item repository.
        /// </summary>
        /// <value>The order line item repository.</value>
        private OrderLineItemRepository<TOrderLineItem> OrderLineItemRepository { get; }

        /// <summary>
        /// Gets the payment method repository.
        /// </summary>
        /// <value>The payment method repository.</value>
        private PaymentMethodRepository<TPaymentMethod> PaymentMethodRepository { get; }

        /// <summary>
        /// Gets the state machine repository.
        /// </summary>
        /// <value>The state machine repository.</value>
        private StateMachineRepository<TStateMachine> StateMachineRepository { get; }

        /// <summary>
        /// Gets the state machine state repository.
        /// </summary>
        /// <value>The state machine state repository.</value>
        private StateMachineStateRepository<TStateMachineState> StateMachineStateRepository { get; }

        /// <summary>
        /// Gets the shipping method repository.
        /// </summary>
        /// <value>The shipping method repository.</value>
        private ShippingMethodRepository ShippingMethodRepository { get; }

        #region Caches

        /// <summary>
        /// The order state machine identifier
        /// </summary>
        private string _orderStateMachineId;

        /// <summary>
        /// The order state machine state identifier cache
        /// </summary>
        private ConcurrentDictionary<string, string> _orderStateMachineStateIdCache;

        /// <summary>
        /// The country cache
        /// </summary>
        private ConcurrentDictionary<string, TCountry> _countryCache;

        /// <summary>
        /// The country state cache
        /// </summary>
        private ConcurrentDictionary<string, TCountryState> _countryStateCache;

        /// <summary>
        /// The customer group cache
        /// </summary>
        private ConcurrentDictionary<string, TCustomerGroup> _customerGroupCache;

        /// <summary>
        /// The currency cache
        /// </summary>
        private ConcurrentDictionary<string, TCurrency> _currencyCache;

        /// <summary>
        /// The payment method cache
        /// </summary>
        private ConcurrentDictionary<string, TPaymentMethod> _paymentMethodCache;

        /// <summary>
        /// The shipping method cache
        /// </summary>
        private ConcurrentDictionary<string, ShippingMethod> _shippingMethodCache;

        /// <summary>
        /// The state machine state cache
        /// </summary>
        private ConcurrentDictionary<string, TStateMachineState> _stateMachineStateCache;

        /// <summary>
        /// Clears the caches.
        /// </summary>
        public void ClearCaches()
        {
            this._orderStateMachineId = null;
            this._orderStateMachineStateIdCache = new ConcurrentDictionary<string, string>();
            this._countryCache = new ConcurrentDictionary<string, TCountry>();
            this._countryStateCache = new ConcurrentDictionary<string, TCountryState>();
            this._currencyCache = new ConcurrentDictionary<string, TCurrency>();
            this._customerGroupCache = new ConcurrentDictionary<string, TCustomerGroup>();
            this._paymentMethodCache = new ConcurrentDictionary<string, TPaymentMethod>();
            this._shippingMethodCache = new ConcurrentDictionary<string, ShippingMethod>();
            this._stateMachineStateCache = new ConcurrentDictionary<string, TStateMachineState>();
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderReader" /> class.
        /// </summary>
        /// <param name="countryRepository">The country repository.</param>
        /// <param name="countryStateRepository">The country state repository.</param>
        /// <param name="currencyRepository">The currency repository.</param>
        /// <param name="customerRepository">The customer repository.</param>
        /// <param name="customerGroupRepository">The customer group repository.</param>
        /// <param name="orderRepository">The order repository.</param>
        /// <param name="orderLineItemRepository">The order line item repository.</param>
        /// <param name="paymentMethodRepository">The payment method repository.</param>
        /// <param name="stateMachineRepository">The state machine repository.</param>
        /// <param name="stateMachineStateRepository">The state machine state repository.</param>
        /// <param name="shippingMethodRepository">The shipping method repository.</param>
        public OrderReader(
            CountryRepository<TCountry> countryRepository,
            CountryStateRepository<TCountryState> countryStateRepository,
            CurrencyRepository<TCurrency> currencyRepository,
            CustomerRepository<TCustomer> customerRepository,
            CustomerGroupRepository<TCustomerGroup> customerGroupRepository,
            OrderRepository<TOrder> orderRepository,
            OrderLineItemRepository<TOrderLineItem> orderLineItemRepository,
            PaymentMethodRepository<TPaymentMethod> paymentMethodRepository,
            StateMachineRepository<TStateMachine> stateMachineRepository,
            StateMachineStateRepository<TStateMachineState> stateMachineStateRepository,
            ShippingMethodRepository shippingMethodRepository)
        {
            this.CountryRepository = countryRepository;
            this.CountryStateRepository = countryStateRepository;
            this.CurrencyRepository = currencyRepository;
            this.CustomerRepository = customerRepository;
            this.CustomerGroupRepository = customerGroupRepository;
            this.OrderRepository = orderRepository;
            this.OrderLineItemRepository = orderLineItemRepository;
            this.PaymentMethodRepository = paymentMethodRepository;
            this.ShippingMethodRepository = shippingMethodRepository;
            this.StateMachineRepository = stateMachineRepository;
            this.StateMachineStateRepository = stateMachineStateRepository;

            this.ClearCaches();
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <param name="id">The country identifier.</param>
        /// <returns>TCountry.</returns>
        private async Task<TCountry> GetCountry(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            
            if(this._countryCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var country = await this.CountryRepository.Read(id).ConfigureAwait(false);
            if (country == null)
            {
                return null;
            }

            this._countryCache.TryAdd(id, country);
            return country;
        }

        /// <summary>
        /// Gets the state of the country.
        /// </summary>
        /// <param name="id">The state of the country identifier.</param>
        /// <returns>TCountryState.</returns>
        private async Task<TCountryState> GetCountryState(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            if (this._countryStateCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var countryState = await this.CountryStateRepository.Read(id).ConfigureAwait(false);
            if (countryState == null)
            {
                return null;
            }
            this._countryStateCache.TryAdd(id, countryState);
            return countryState;
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <param name="id">The currency identifier.</param>
        /// <returns>TCurrency.</returns>
        private async Task<TCurrency> GetCurrency(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            if (this._currencyCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var currency = await this.CurrencyRepository.Read(id).ConfigureAwait(false);
            if (currency == null)
            {
                return null;
            }
            this._currencyCache.TryAdd(id, currency);
            return currency;
        }

        /// <summary>
        /// Gets the customer group.
        /// </summary>
        /// <param name="id">The customer group identifier.</param>
        /// <returns>TCustomerGroup.</returns>
        private async Task<TCustomerGroup> GetCustomerGroup(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            if (this._customerGroupCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var customerGroup = await this.CustomerGroupRepository.Read(id).ConfigureAwait(false);
            if (customerGroup == null)
            {
                return null;
            }
            this._customerGroupCache.TryAdd(id, customerGroup);
            return customerGroup;
        }

        /// <summary>
        /// Gets the state of the state machine.
        /// </summary>
        /// <param name="id">The state machine identifier.</param>
        /// <returns>TStateMachineState.</returns>
        private async Task<TStateMachineState> GetStateMachineState(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            if (this._stateMachineStateCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var stateMachineState = await this.StateMachineStateRepository.Read(id).ConfigureAwait(false);
            if (stateMachineState == null)
            {
                return null;
            }
            this._stateMachineStateCache.TryAdd(id, stateMachineState);
            return stateMachineState;
        }

        /// <summary>
        /// Gets the payment method.
        /// </summary>
        /// <param name="id">The payment method identifier.</param>
        /// <returns>TPaymentMethod.</returns>
        private async Task<TPaymentMethod> GetPaymentMethod(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            if (this._paymentMethodCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var paymentMethod = await this.PaymentMethodRepository.Read(id).ConfigureAwait((false));
            if (paymentMethod == null)
            {
                return null;
            }
            this._paymentMethodCache.TryAdd(id, paymentMethod);
            return paymentMethod;
        }

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <param name="id">The shipping method identifier.</param>
        /// <returns>ShippingMethod.</returns>
        private async Task<ShippingMethod> GetShippingMethod(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            if (this._shippingMethodCache.TryGetValue(id, out var result))
            {
                return result;
            }

            var shippingMethod = await this.ShippingMethodRepository.Read(id).ConfigureAwait(false);
            if (shippingMethod == null)
            {
                return null;
            }
            this._shippingMethodCache.TryAdd(id, shippingMethod);
            return shippingMethod;
        }

        /// <summary>
        /// Get the state machine id for orders.
        /// </summary>
        /// <returns>System.String.</returns>
        private async Task<string> GetOrderStateMachineId()
        {
            if (!string.IsNullOrWhiteSpace(this._orderStateMachineId))
            {
                return this._orderStateMachineId;
            }


            // Zustandsmaschine für Bestellzustände ermitteln
            var stateMachines = await this.StateMachineRepository.ReadIds(new Search
            {
                Limit = 1,
                Page = 1,
                Filters = new[] {
                    new Filter {
                        Field = "technicalName",
                        Type = "equals",
                        Value = "order.state"
                    }
                }
            }).ConfigureAwait(false);
            this._orderStateMachineId = stateMachines?.Items?.FirstOrDefault();

            return this._orderStateMachineId;
        }

        /// <summary>
        /// Gets the state identifier for a state technical name.
        /// </summary>
        /// <param name="technicalName">The technical state name.</param>
        /// <returns>System.String.</returns>
        private async Task<string> GetOrderStateMachineStateId(string technicalName)
        {
            if (string.IsNullOrWhiteSpace(technicalName))
            {
                return null;
            }
            if (this._orderStateMachineStateIdCache.TryGetValue(technicalName, out var result))
            {
                return result;
            }

            var orderStateMachineId = await this.GetOrderStateMachineId().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(orderStateMachineId))
            {
                return null;
            }

            // Zustand für offene Bestellaufträge
            var stateMachineStates = await this.StateMachineStateRepository.ReadIds(new Search
            {
                Limit = 10,
                Page = 1,
                Filters = new[] {
                    new Filter {
                        Field = "stateMachineId",
                        Type = "equals",
                        Value = orderStateMachineId
                    },
                    new Filter {
                        Field = "technicalName",
                        Type = "equals",
                        Value = technicalName
                    },
                }
            }).ConfigureAwait(false);

            var stateId = stateMachineStates?.Items?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(stateId))
            {
                return null;
            }
            this._orderStateMachineStateIdCache.TryAdd(technicalName, stateId);
            return stateId;
        }

        /// <summary>
        /// Liefert eine Liste mit IDs zu offenen Bestellungen zurück.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        private async Task<List<string>> ReadOrderIds(string state)
        {
            //
            // Read state machine state id for orders.
            //
            var orderStateMachineStateId = await this.GetOrderStateMachineStateId(state).ConfigureAwait(false);
            if (orderStateMachineStateId == null)
            {
                return new List<string>();
            }

            var limit = 100;
            var result = await this.OrderRepository.ReadAllIds(new Search
            {
                Page = 1,
                Limit = limit,
                TotalCountMode = TotalCountMode.Exact,
                Filters = new[] {
                    new Filter {
                        Field = "stateId",
                        Value = orderStateMachineStateId,
                        Type = "equals"
                    }
                }
            }).ConfigureAwait(false);

            return result ?? new List<string>();
        }

        /// <summary>
        /// Reads the order identifiers by order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private async Task<List<string>> ReadOrderIdsByOrderNumber(string orderNumber)
        {
            var limit = 100;
            var result = await this.OrderRepository.ReadAllIds(new Search
            {
                Page = 1,
                Limit = limit,
                TotalCountMode = TotalCountMode.Exact,
                Filters = new[] {
                    new Filter {
                        Field = "orderNumber",
                        Value = orderNumber,
                        Type = "equals"
                    }
                }
            }).ConfigureAwait(false);

            return result ?? new List<string>();
        }

        /// <summary>
        /// Enriches the customer address with country and country state data.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>CustomerAddress.</returns>
        private async Task<CustomerAddress> EnrichCustomerAddress(CustomerAddress address)
        {
            if (address == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(address.CountryId))
            {
                address.Country = await this.GetCountry(address.CountryId).ConfigureAwait(false);
            }
            if (!string.IsNullOrWhiteSpace(address.CountryStateId))
            {
                address.CountryState = await this.GetCountryState(address.CountryStateId).ConfigureAwait(false);
            }
            return address;
        }

        /// <summary>
        /// Reads a a complete order by identifier.
        /// </summary>
        /// <param name="id">Die Shopware ID.</param>
        /// <returns>Order.</returns>
        private async Task<TOrder> ReadInternal(string id)
        {
            var order = await this.OrderRepository.Read(id).ConfigureAwait(false);
            if (order == null)
            {
                return null;
            }

            // Get currency
            if (string.IsNullOrWhiteSpace(order.CurrencyId))
            {
                return null;
            }
            order.Currency = await this.GetCurrency(order.CurrencyId).ConfigureAwait(false);

            // Get state machine state
            if (string.IsNullOrWhiteSpace(order.StateId))
            {
                return null;
            }
            order.StateMachineState = await this.GetStateMachineState(order.StateId).ConfigureAwait(false);

            // Get customer data
            if (order.OrderCustomer == null || string.IsNullOrWhiteSpace(order.OrderCustomer.CustomerId))
            {
                return null;
            }
            order.OrderCustomer.Customer = await this.CustomerRepository.Read(order.OrderCustomer.CustomerId).ConfigureAwait(false);
            if (order.OrderCustomer.Customer != null)
            {
                // customer group
                if (!string.IsNullOrWhiteSpace(order.OrderCustomer.Customer.GroupId))
                {
                    order.OrderCustomer.Customer.Group = await this.GetCustomerGroup(order.OrderCustomer.Customer.GroupId).ConfigureAwait(false);
                }

                // customers payment method
                if (!string.IsNullOrWhiteSpace(order.OrderCustomer.Customer.DefaultPaymentMethodId))
                {
                    order.OrderCustomer.Customer.DefaultPaymentMethod =
                        await this.GetPaymentMethod(order.OrderCustomer.Customer.DefaultPaymentMethodId).ConfigureAwait(false);
                }

                // read customer addresses
                var addresses = await this.CustomerRepository.ReadProperty<List<CustomerAddress>>(order.OrderCustomer.CustomerId,
                        "addresses").ConfigureAwait(false) ?? new List<CustomerAddress>();
                addresses.ForEach(async v => await this.EnrichCustomerAddress(v));
                order.OrderCustomer.Customer.Addresses = addresses;

                // read default billing address
                var defaultBillingAddress = (await this.CustomerRepository.ReadProperty<List<CustomerAddress>>(order.OrderCustomer.CustomerId, "defaultBillingAddress").ConfigureAwait(false))?.FirstOrDefault();
                order.OrderCustomer.Customer.DefaultBillingAddress = await this.EnrichCustomerAddress(defaultBillingAddress);

                // default shipping address
                var defaultShippingAddress = (await this.CustomerRepository.ReadProperty<List<CustomerAddress>>(order.OrderCustomer.CustomerId,
                        "defaultShippingAddress").ConfigureAwait(false))?.FirstOrDefault();
                order.OrderCustomer.Customer.DefaultShippingAddress = await this.EnrichCustomerAddress(defaultShippingAddress);
            }

            // Get delivery data.
            order.Deliveries = await this.OrderRepository.ReadProperty<List<OrderDelivery>>(id, "deliveries").ConfigureAwait(false);
            if (order.Deliveries != null)
            {
                foreach (var delivery in order.Deliveries)
                {
                    // get shipping method
                    delivery.ShippingMethod = await this.GetShippingMethod(delivery.ShippingMethodId).ConfigureAwait(false);

                    if (delivery.ShippingOrderAddress == null)
                    {
                        continue;
                    }
                    delivery.StateMachineState = await this.GetStateMachineState(delivery.StateId).ConfigureAwait(false);

                    // ... country and state.
                    delivery.ShippingOrderAddress.Country =
                        await this.GetCountry(delivery.ShippingOrderAddress.CountryId).ConfigureAwait(false);
                    delivery.ShippingOrderAddress.CountryState =
                        await this.GetCountryState(delivery.ShippingOrderAddress.CountryStateId).ConfigureAwait(false);
                }
            }

            // Get billing address
            order.BillingAddress = (await this.OrderRepository.ReadProperty<List<OrderAddress>>(id, "billingAddress").ConfigureAwait(false))?.FirstOrDefault();
            if (order.BillingAddress != null)
            {
                // ... country and state.
                order.BillingAddress.Country = await this.GetCountry(order.BillingAddress.CountryId).ConfigureAwait(false);
                order.BillingAddress.CountryState = await this.GetCountryState(order.BillingAddress.CountryStateId).ConfigureAwait(false);

            }

            // Get transactions
            order.Transactions = await this.OrderRepository.ReadProperty<List<OrderTransaction>>(id, "transactions").ConfigureAwait(false);
            if (order.Transactions != null)
            {
                foreach (var transaction in order.Transactions)
                {
                    // Zahlungsmethoden
                    transaction.PaymentMethod = await this.GetPaymentMethod(transaction.PaymentMethodId).ConfigureAwait(false);
                    transaction.StateMachineState = await this.GetStateMachineState(transaction.StateId).ConfigureAwait(false);
                }
            }

            // Get order lines
            order.LineItems = await this.OrderRepository.ReadProperty<List<OrderLineItem>>(id, "lineItems").ConfigureAwait(false);
            if (order.LineItems != null)
            {
                foreach (var orderLineItem in order.LineItems)
                {
                    // read product
                    orderLineItem.Product = (await this.OrderLineItemRepository.ReadProperty<List<Product>>(orderLineItem.Id, "product").ConfigureAwait(false))
                        .FirstOrDefault();
                }
            }
            return order;
        }

        /// <summary>
        /// Validates an order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns><c>true</c> if all order data is available, <c>false</c> otherwise.</returns>
        private bool Validate(Order order)
        {
            if (order == null)
            {
                return false;
            }

            if (order.Currency == null)
            {
                return false;
            }

            if (order.OrderCustomer == null)
            {
                return false;
            }

            if (order.OrderCustomer.Customer == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads an order by shopware id.
        /// </summary>
        /// <param name="id">The shopware identifier.</param>
        /// <returns>Order.</returns>
        public async Task<Order> Read(string id)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));

            var order = await this.ReadInternal(id).ConfigureAwait(false);
            if (order == null || !this.Validate(order))
            {
                return null;
            }
            return order;
        }

        /// <summary>
        /// Reads an order the by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>List&lt;Order&gt;.</returns>
        public async Task<List<Order>> ReadByOrderNumber(string orderNumber)
        {
            var result = new List<Order>();

            var idList = await this.ReadOrderIdsByOrderNumber(orderNumber).ConfigureAwait(false);

            foreach (var id in idList)
            {
                var order = await this.ReadInternal(id).ConfigureAwait(false);
                if (order == null)
                {
                    continue;
                }
                if (this.Validate(order))
                {
                    result.Add(order);
                }
            }
            return result;
        }

        /// <summary>
        /// Reads orders by the order state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>List&lt;Order&gt;.</returns>
        public async Task<List<Order>> ReadByState(string state)
        {
            var result = new List<Order>();

            var idList = await this.ReadOrderIds(state).ConfigureAwait(false);

            foreach (var id in idList)
            {
                var order = await this.ReadInternal(id).ConfigureAwait(false);
                if (order == null)
                {
                    continue;
                }
                if (this.Validate(order))
                {
                    result.Add(order);
                }
            }
            return result;
        }

        /// <summary>
        /// Reads the open orders.
        /// </summary>
        /// <returns>List&lt;Order&gt;.</returns>
        public async Task<List<Order>> ReadOpen()
        {
            return await this.ReadByState("open").ConfigureAwait(false);
        }
    }
}