using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Services
{
    public class VersionServiceTests : BaseTest
    {
        [Fact()]
        public async Task TestGetVersion()
        {
            this.Setup();
            try
            {
                var client = this.TestContext.CreateClient();
                var sut = new VersionService(client);
                var version = await sut.GetVersion();
                Assert.NotNull(version);
                Assert.NotNull(version.Value);
            }
            finally
            {
                this.Cleanup();
            }
        }
    }
}