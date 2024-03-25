using Compori.Shopware.Repositories;
using Compori.Shopware.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Services
{
    public class OrderDocumentServiceTests : BaseTest
    {
        private readonly string TestOrderId = "018e74e2bef3723b9320ee72f246afa7";

        private async Task CleanupAsync()
        {
            var client = this.TestContext.CreateClient();
            var doc = new DocumentRepository(client);

            var documents = await doc.ReadAll(new Search
            {
                Filters = new[]
                {
                        new Filter
                        {
                            Field = "order.id",
                            Type  = "equals",
                            Value = TestOrderId
                        }
                    }
            });

            var documentIds = documents.OrderByDescending(v => v.Value.CreatedAt).ToList().Select(v => v.Key).ToList();
            
            foreach (var documentId in documentIds)
            {
                await doc.Delete(documentId);
            }

            this.Cleanup();
        }

        [Fact()]
        public async Task TestCreate()
        {
            this.Setup();
            try
            {
                var client = this.TestContext.CreateClient();
                var sut = new OrderDocumentService(client);


                var result = await sut.Create(new Types.CreateOrderDocument
                {
                    OrderId = TestOrderId,
                    FileType = "pdf",
                    Config = new Types.CreateOrderDocumentConfig()
                    {
                        DocumentComment = "Test Kommentar",
                        DocumentDate = DateTime.Now.Date,
                        DocumentNumber = "AR2417799",
                        Custom = new Types.CreateOrderDocumentConfigCustom()
                        {
                            InvoiceNumber = "AR2417799"
                        }
                    },
                    Static = true
                }, "invoice");

                Assert.NotNull(result);
            }
            finally
            {
                await this.CleanupAsync();
            }
        }
    }
}