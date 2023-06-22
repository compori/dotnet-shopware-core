using Compori.Shopware.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class TagRepositoryTests : BaseTest
    {
        protected TagRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new TagRepository(this.TestContext.CreateClient());
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
        public async Task TestCreateReadUpdateDelete()
        {
            this.Setup();
            string id = null;
            try
            {
                var name = "Test Tag " + Guid.NewGuid().ToString();
                var tag = new Tag
                {
                    Name = name,
                };
                id = await this.Repository.Create(tag).ConfigureAwait(false);
                Assert.NotNull(id);
                tag.Id = id;

                var actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Equal(tag.Name, actual.Name);
                Assert.Equal(tag.Id, actual.Id);

                tag.Name = "Renamed Tag " + Guid.NewGuid().ToString();
                await this.Repository.Update(tag).ConfigureAwait(false);
                
                actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Equal(tag.Name, actual.Name);
                Assert.Equal(tag.Id, actual.Id);

                await this.Repository.Delete(tag.Id).ConfigureAwait(false);
                actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Null(actual);
                id = null;
            }
            finally
            {
                if(id != null)
                {
                    await this.Repository.Delete(id).ConfigureAwait(false);
                }
                this.Cleanup();
            }
        }
    }
}
