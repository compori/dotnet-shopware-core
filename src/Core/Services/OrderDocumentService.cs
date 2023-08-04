using Compori.Shopware.Types;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware.Services
{
    public class OrderDocumentService
    {
        /// <summary>
        /// Gets the shopware rest client.
        /// </summary>
        /// <value>The shopware rest client.</value>
        protected Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDocumentService"/> class.
        /// </summary>
        /// <param name="client">The shopware rest client.</param>
        public OrderDocumentService(Client client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Creates a new delivery note document for a shopware order.
        /// </summary>
        /// <param name="id">The shopware order identifier.</param>
        /// <param name="number">The delivery note number.</param>
        /// <param name="date">The document date.</param>
        /// <param name="deliveryDate">The delivery date.</param>
        /// <param name="comment">A document comment.</param>
        /// <param name="isCustomDocument">If <c>true</c> a custom delivery note document is supposed to be used.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OrderDocument.</returns>
        public async Task<OrderDocument> CreateDeliveryNote(
            string id, 
            string number, 
            DateTime date, 
            DateTime deliveryDate, 
            string comment, 
            bool isCustomDocument, 
            CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(number, nameof(number));

            var orderDocument = new CreateOrderDocument()
            {
                Config = new CreateOrderDocumentConfig()
                {
                    DocumentComment = comment,
                    DocumentDate = date,
                    DocumentNumber = number,
                    Custom = new CreateOrderDocumentConfigCustom()
                    {
                        DeliveryNoteDate = date,
                        DeliveryNoteNumber = number,
                        DeliveryDate = deliveryDate
                    }
                },
                Static = isCustomDocument
            };
            return await this.Client.Post<CreateOrderDocument, OrderDocument>(
                $"_action/order/{id}/document/delivery_note", 
                orderDocument, 
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Erstellt ein new invoice document for a shopware order.
        /// </summary>
        /// <param name="id">The shopware order identifier.</param>
        /// <param name="number">The invoice number.</param>
        /// <param name="date">The document date.</param>
        /// <param name="comment">A document comment.</param>
        /// <param name="isCustomDocument">If <c>true</c> a custom delivery note document is supposed to be used.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OrderDocument.</returns>
        public async Task<OrderDocument> CreateInvoice(
            string id, 
            string number, 
            DateTime date, 
            string comment, 
            bool isCustomDocument, 
            CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(number, nameof(number));
            var orderDocument = new CreateOrderDocument()
            {
                Config = new CreateOrderDocumentConfig()
                {
                    DocumentComment = comment,
                    DocumentDate = date,
                    DocumentNumber = number,
                    Custom = new CreateOrderDocumentConfigCustom()
                    {
                        InvoiceNumber = number
                    }
                },
                Static = isCustomDocument
            };
            return await this.Client.Post<CreateOrderDocument, OrderDocument>(
                $"_action/order/{id}/document/invoice", 
                orderDocument,
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Erstellt ein new storno invoice document for a shopware order.
        /// </summary>
        /// <param name="id">The shopware order identifier.</param>
        /// <param name="number">The storno invoice number.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <param name="referenceDocumentId">The reference document identifier to the invoice.</param>
        /// <param name="date">The document date.</param>
        /// <param name="comment">A document comment.</param>
        /// <param name="isCustomDocument">If <c>true</c> a custom delivery note document is supposed to be used.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OrderDocument.</returns>
        public async Task<OrderDocument> CreateStornoInvoice(
            string id, 
            string number, 
            string invoiceNumber,
            string referenceDocumentId,
            DateTime date, 
            string comment, 
            bool isCustomDocument, 
            CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(number, nameof(number));
            var orderDocument = new CreateOrderDocument()
            {
                Config = new CreateOrderDocumentConfig()
                {
                    DocumentComment = comment,
                    DocumentDate = date,
                    DocumentNumber = number,
                    Custom = new CreateOrderDocumentConfigCustom()
                    {
                        InvoiceNumber = invoiceNumber,
                        StornoNumber = number
                    }
                },
                Static = isCustomDocument,
                ReferencedDocumentId = referenceDocumentId
            };
            return await this.Client.Post<CreateOrderDocument, OrderDocument>(
                $"_action/order/{id}/document/storno",
                orderDocument, 
                cancellationToken).ConfigureAwait (false); 
        }
    }
}
