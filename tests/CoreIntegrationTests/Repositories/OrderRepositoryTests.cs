using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class OrderRepositoryTests : BaseTest
    {
        protected OrderRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new OrderRepository(this.TestContext.CreateClient());
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

        
        [Fact(Skip = "Skipping - Adhoc testing.")]
        public async Task TestRead2()
        {
            this.Setup();
            try
            {
                var sut =  new OrderRepository(this.TestContext.CreateClient("testing-shop-2.ignore.json"));
                var item = await sut.Read("018e7986b22671029598006f97025b9c").ConfigureAwait(false);
                Assert.NotNull(item);
            }
            finally
            {
                this.Cleanup();
            }
        }
    }
}