using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class AppPaymentMethodRepositoryTests : BaseTest
    {
        protected AppPaymentMethodRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new AppPaymentMethodRepository(this.TestContext.CreateClient());
        }

        [Fact()]
        public async Task TestReadAsync()
        {
            this.Setup();
            try
            {
                var items = await this.Repository.Read(new Types.Search { Limit = 25 }).ConfigureAwait(false);
                Assert.NotNull(items);
            }
            finally
            {
                this.Cleanup();
            }
        }
    }
}
