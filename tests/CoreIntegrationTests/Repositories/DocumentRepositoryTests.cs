using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class DocumentRepositoryTests : BaseTest
    {
        protected DocumentRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new DocumentRepository(this.TestContext.CreateClient());
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