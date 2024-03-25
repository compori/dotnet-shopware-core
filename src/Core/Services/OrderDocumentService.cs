using Compori.Shopware.Types;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Creates documents for a specified document type.
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="type">The type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List&lt;OrderDocument&gt;.</returns>
        private async Task<List<OrderDocument>> Create(List<CreateOrderDocument> documents, string type, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(documents, nameof(documents));
            Guard.AssertArgumentIsNotNull(type, nameof(type));

            if (documents.Count == 0)
            {
                return new List<OrderDocument>();
            }

            return (await this.Client.Post<List<CreateOrderDocument>, DataResponse<List<OrderDocument>>>(
                $"_action/order/document/{type}/create",
                documents,
                cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <summary>
        /// Creates a document for a specified document type.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OrderDocument.</returns>
        public async Task<OrderDocument> Create(CreateOrderDocument document, string type, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(document, nameof(document));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(type, nameof(type));

            return (await this.Create(new List<CreateOrderDocument>() { document }, type, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }

        /// <summary>
        /// Creates multiple documents.
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List&lt;OrderDocument&gt;.</returns>
        public async Task<List<OrderDocument>> Create(List<CreateOrderDocument> documents, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(documents, nameof(documents));

            var result = new List<OrderDocument>();
            if (documents.Count == 0)
            {
                return result;
            }

            var types = new[] { "delivery_note", "invoice", "storno" };
            foreach (var type in types)
            {
                result.AddRange(await this.Create(documents.Where(v => "type".Equals(v.Type)).ToList(), type, cancellationToken).ConfigureAwait(false));
            }

            return result;
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
        public async Task<OrderDocument> CreateDeliveryNote(string id, string number, DateTime date, DateTime deliveryDate, string comment, bool isCustomDocument,
            CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(number, nameof(number));

            var orderDocument = new CreateOrderDocument()
            {
                OrderId = id,
                ReferencedDocumentId = null,
                Config = new CreateOrderDocumentConfig()
                {
                    Custom = new CreateOrderDocumentConfigCustom()
                    {
                        DeliveryNoteDate = date,
                        DeliveryNoteNumber = number,
                        DeliveryDate = deliveryDate
                    },
                    DocumentComment = comment,
                    DocumentDate = date,
                    DocumentNumber = number,
                },
                Static = isCustomDocument
            };

            return await this.Create(orderDocument, "delivery_note", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new invoice document for a shopware order.
        /// </summary>
        /// <param name="id">The shopware order identifier.</param>
        /// <param name="number">The invoice number.</param>
        /// <param name="date">The document date.</param>
        /// <param name="comment">A document comment.</param>
        /// <param name="isCustomDocument">If <c>true</c> a custom invoice document is supposed to be used.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OrderDocument.</returns>
        public async Task<OrderDocument> CreateInvoice(string id, string number, DateTime date, string comment, bool isCustomDocument,
            CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(number, nameof(number));
            var orderDocument = new CreateOrderDocument()
            {
                OrderId = id,
                ReferencedDocumentId = null,
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

            return await this.Create(orderDocument, "invoice", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new storno document for a shopware order.
        /// </summary>
        /// <param name="id">The shopware order identifier.</param>
        /// <param name="number">The storno number.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <param name="referenceDocumentId">The reference document identifier to the invoice.</param>
        /// <param name="date">The document date.</param>
        /// <param name="comment">A document comment.</param>
        /// <param name="isCustomDocument">If <c>true</c> a custom delivery note document is supposed to be used.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OrderDocument.</returns>
        public async Task<OrderDocument> CreateStorno(string id, string referenceDocumentId, string number, string invoiceNumber, DateTime date, string comment, bool isCustomDocument,
            CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(id, nameof(id));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(referenceDocumentId, nameof(referenceDocumentId));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(number, nameof(number));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(invoiceNumber, nameof(invoiceNumber));

            var orderDocument = new CreateOrderDocument()
            {
                OrderId = id,
                ReferencedDocumentId = referenceDocumentId,
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
            };
            return await this.Create(orderDocument, "storno", cancellationToken).ConfigureAwait(false);
        }
    }
}
