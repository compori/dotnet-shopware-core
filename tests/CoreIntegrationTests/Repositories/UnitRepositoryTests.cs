using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class UnitRepositoryTests : BaseTest
    {
        protected UnitRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new UnitRepository(this.TestContext.CreateClient());
        }

        [Fact()]
        public async Task TestRead()
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
