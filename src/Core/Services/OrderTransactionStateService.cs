using Compori.Shopware.Entities;
using Compori.Shopware.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Services
{
    public class OrderTransactionStateService : OrderTransactionStateService<StateMachineState>
    {
        public OrderTransactionStateService(Client client) : base(client)
        {
        }
    }

    public class OrderTransactionStateService<TStateMachineState> where TStateMachineState : StateMachineState
    {
        /// <summary>
        /// Gets the shopware rest client.
        /// </summary>
        /// <value>The shopware rest client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderTransactionStateService"/> class.
        /// </summary>
        /// <param name="client">The shopware rest client.</param>
        public OrderTransactionStateService(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Modifies order state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="transition">New State.</param>
        /// <param name="orderState">Statedetails.</param>
        /// <returns>StateMachineState.</returns>
        public async Task<TStateMachineState> Set(string id, string transition, OrderState orderState = null, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(transition, nameof(transition));
            return await this.Client.Post<OrderState, TStateMachineState>($"_action/order_transaction/{id}/state/{transition}",
                orderState ?? new OrderState(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the reopen.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <returns>StateMachineState.</returns>
        public async Task<TStateMachineState> Reopen(string id, bool sendMail, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));

            // new state should be open
            return await this.Set(id, "reopen",
                sendMail
                ? new OrderState
                {
                    DocumentIds = null,
                    SendMail = sendMail
                }
                : null, cancellationToken).ConfigureAwait(!false);
        }

        /// <summary>
        /// Tries the reopen the order transaction.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if transaction state is open after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryReopen(string id, bool sendMail, CancellationToken cancellationToken = default)
        {
            return "open".Equals((await this.Reopen(id, sendMail, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }

        /// <summary>
        /// Sets the remind.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. proforma invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <returns>StateMachineState.</returns>
        public async Task<TStateMachineState> Remind(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));

            // reminded
            return await this.Set(id, "remind", new OrderState
            {
                DocumentIds = !string.IsNullOrWhiteSpace(documentId)
                    ? new List<string>() { documentId }
                    : null,
                SendMail = sendMail
            },
            cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Tries to set die remind state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. proforma invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if transaction state is reminded after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryRemind(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            return "reminded".Equals((await this.Remind(id, documentId, sendMail, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }

        /// <summary>
        /// Sets the remind.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <returns>StateMachineState.</returns>
        public async Task<TStateMachineState> PayPartially(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));

            // paid_partially
            return await this.Set(id, "pay_partially", new OrderState
            {

                DocumentIds = !string.IsNullOrWhiteSpace(documentId)
                    ? new List<string>() { documentId }
                    : null,
                SendMail = sendMail
            },
            cancellationToken);
        }

        /// <summary>
        /// Tries to sets the remind transaction state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if transaction state is payed partially after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryPayPartially(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            return "payed_partially".Equals((await this.PayPartially(id, documentId, sendMail, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }

        /// <summary>
        /// Sets the paid state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>StateMachineState.</returns>
        public async Task<TStateMachineState> Pay(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            return await this.Set(id, "pay", new OrderState
            {

                DocumentIds = !string.IsNullOrWhiteSpace(documentId)
                    ? new List<string>() { documentId }
                    : null,
                SendMail = sendMail
            },
            cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Tries tos sets the paid state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if transaction state is payed after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryPay(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            return "payed".Equals((await this.Pay(id, documentId, sendMail, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }

        /// <summary>
        /// Sets the refund state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>StateMachineState.</returns>
        public async Task<TStateMachineState> Refund(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            return await this.Set(id, "refund", new OrderState
            {

                DocumentIds = !string.IsNullOrWhiteSpace(documentId)
                    ? new List<string>() { documentId }
                    : null,
                SendMail = sendMail
            },
            cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Tries tos sets the refund state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="documentId">A optional document identifier e.g. invoice.</param>
        /// <param name="sendMail">if set to <c>true</c> if costumer should receive a notfication mail.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if transaction state is refunded after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryRefund(string id, string documentId, bool sendMail, CancellationToken cancellationToken = default)
        {
            return "refunded".Equals((await this.Pay(id, documentId, sendMail, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }
    }
}