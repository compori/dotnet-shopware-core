using Xunit;
using Compori.Shopware.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compori.Shopware.Factories;

namespace Compori.Shopware.Services
{
    public class VersionServiceTests : BaseTest
    {
        [Fact()]
        public async Task TestGetVersionAsync()
        {
            this.Setup();
            try
            {
                var client = this.TestContext.CreateClient();
                var sut = new VersionService(client);
                var version = await sut.GetVersionAsync();
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