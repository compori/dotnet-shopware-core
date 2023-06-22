using Compori.Shopware.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class ShippingMethodRepositoryTests : BaseTest
    {
        protected ShippingMethodRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = this.TestContext.CreateShippingMethodRepository();
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

        [Fact()]
        public async Task TestTagging()
        {
            this.Setup();
            Tag tag1 = null;
            Tag tag2 = null;
            try
            {
                tag1 = await this.TestContext.CreateTag();
                tag2 = await this.TestContext.CreateTag();

                var items = await this.Repository.Read(new Types.Search { Limit = 25 }).ConfigureAwait(false);
                Assert.NotNull(items);

                var item = items.Items.OrderBy(v => v.Id).First();
                var tags = await this.Repository.ReadTags(item.Id);
                Assert.NotNull(tags);
                Assert.Empty(tags);

                await this.Repository.AddTag(item.Id, tag1.Id);
                tags = await this.Repository.ReadTags(item.Id);
                Assert.NotNull(tags);
                Assert.Single(tags);
                Assert.Equal(tag1.Id, tags[0].Id);
                Assert.Equal(tag1.Name, tags[0].Name);

                tags = await this.Repository.ReplaceTags(item.Id, new List<string>{ tag2.Id });
                Assert.NotNull(tags);
                Assert.Single(tags);
                Assert.Equal(tag2.Id, tags[0].Id);
                Assert.Equal(tag2.Name, tags[0].Name);

                await this.Repository.RemoveTags(item.Id, new List<string>{ tag2.Id });
                tags = await this.Repository.ReadTags(item.Id);
                Assert.NotNull(tags);
                Assert.Empty(tags);
            }
            finally
            {
                if (tag1 != null)
                {
                    await this.TestContext.DeleteTag(tag1.Id);
                }
                if (tag2 != null)
                {
                    await this.TestContext.DeleteTag(tag2.Id);
                }
                this.Cleanup();
            }
        }

    }
}