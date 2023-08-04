using Compori.Shopware.Entities;
using Compori.Shopware.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Services
{
    public class OrderStateService : OrderStateService<StateMachineState>
    {
        public OrderStateService(Client client) : base(client)
        {
        }
    }

    public class OrderStateService<TStateMachineState> where TStateMachineState : StateMachineState
    {
        /// <summary>
        /// Gets the shopware rest client.
        /// </summary>
        /// <value>The shopware rest client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStateService"/> class.
        /// </summary>
        /// <param name="client">The shopware rest client.</param>
        public OrderStateService(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Change order state.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="transition">The transistion.</param>
        /// <param name="orderState">The transistion properties.</param>
        public async Task<TStateMachineState> Set(string id, string transition, OrderState orderState = null, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(transition, nameof(transition));

            return await this.Client.Post<OrderState, TStateMachineState>(
                $"_action/order/{id}/state/{transition}",
                orderState ?? new OrderState(),
                cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Set the order state to in_progress.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>TStateMachineState.</returns>
        public async Task<TStateMachineState> Process(string id, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            return await this.Set(id, "process", null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the order state to in_progress.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if state is in_progress after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryProcess(string id, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            return "in_progress".Equals((await this.Process(id, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }

        /// <summary>
        /// Set the order state to cancelled.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="notifyCustomer">If <c>true</c> customer will be notified.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>TStateMachineState.</returns>
        public async Task<TStateMachineState> Cancel(string id, bool notifyCustomer, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            return await this.Set(id, "cancel", new OrderState
            {
                SendMail = notifyCustomer
            },
            cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the order state to cancelled.
        /// </summary>
        /// <param name="id">The shopware order id.</param>
        /// <param name="notifyCustomer">If <c>true</c> customer will be notified.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if state is cancelled after setting state, <c>false</c> otherwise.</returns>
        public async Task<bool> TryCancel(string id, bool notifyCustomer, CancellationToken cancellationToken = default)
        {
            return "cancelled".Equals((await this.Cancel(id, notifyCustomer, cancellationToken).ConfigureAwait(false))?.TechnicalName);
        }
    }
}
