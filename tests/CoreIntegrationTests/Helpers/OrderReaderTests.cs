using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Helpers
{
    public class OrderReaderTests : BaseTest
    {
        protected OrderReader Reader { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Reader = this.TestContext.CreateOrderReader();
        }

        [Fact()]
        public async Task TestReadOpen()
        {
            this.Setup();
            try
            {
                var items = await this.Reader.ReadOpen();
                Assert.NotNull(items);
            }
            finally
            {
                this.Cleanup();
            }
        }
    }
}
