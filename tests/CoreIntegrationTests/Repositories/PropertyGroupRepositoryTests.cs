using Compori.Shopware.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class PropertyGroupRepositoryTests : BaseTest
    {
        protected PropertyGroupRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new PropertyGroupRepository(this.TestContext.CreateClient());
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

        [Fact()]
        public async Task TestCreateReadUpdateDelete()
        {
            this.Setup();
            string id = null;
            try
            {
                var name = "PropGroup " + Guid.NewGuid().ToString();
                var group = new PropertyGroup
                {
                    Name = name,
                    DisplayType = "select",
                    SortingType = "alphanumeric",
                    Filterable = false,
                    VisibleOnProductDetailPage = false,
                };
                id = await this.Repository.Create(group).ConfigureAwait(false);
                Assert.NotNull(id);
                group.Id = id;

                var actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Equal(group.Name, actual.Name);
                Assert.Equal(group.Id, actual.Id);

                group.Name = "Renamed Group " + Guid.NewGuid().ToString();
                await this.Repository.Update(group).ConfigureAwait(false);

                actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Equal(group.Name, actual.Name);
                Assert.Equal(group.Id, actual.Id);

                await this.Repository.Delete(group.Id).ConfigureAwait(false);
                actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Null(actual);
                id = null;
            }
            finally
            {
                if (id != null)
                {
                    await this.Repository.Delete(id).ConfigureAwait(false);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestCreateReadUpdateDeleteMass()
        {
            this.Setup();
            string id1 = null;
            string id2 = Guid.NewGuid().ToString("N");
            try
            {
                var name1 = "PropGroup " + Guid.NewGuid().ToString();
                var group1 = new PropertyGroup
                {
                    Id = id1,
                    Name = name1,
                    DisplayType = "select",
                    SortingType = "alphanumeric",
                    Filterable = false,
                    VisibleOnProductDetailPage = false,
                };
                var name2 = "PropGroup " + Guid.NewGuid().ToString();
                var group2 = new PropertyGroup
                {
                    Id = id2,
                    Name = name2,
                    DisplayType = "select",
                    SortingType = "alphanumeric",
                    Filterable = false,
                    VisibleOnProductDetailPage = false,
                };
                await this.Repository.Update(new List<PropertyGroup> { group1, group2 }).ConfigureAwait(false);
                id1 = group1.Id;
                var actual1 = await this.Repository.Read(id1).ConfigureAwait(false);
                Assert.Equal(group1.Name, actual1.Name);
                Assert.Equal(group1.Id, actual1.Id);

                var actual2 = await this.Repository.Read(id2).ConfigureAwait(false);
                Assert.Equal(group2.Name, actual2.Name);
                Assert.Equal(group2.Id, actual2.Id);

                group1.Name = "Renamed Group " + Guid.NewGuid().ToString();
                await this.Repository.Update(group1).ConfigureAwait(false);

                actual1 = await this.Repository.Read(id1).ConfigureAwait(false);
                Assert.Equal(group1.Name, actual1.Name);
                Assert.Equal(group1.Id, actual1.Id);

                await this.Repository.Delete(new List<string> { group1.Id, group2.Id } ).ConfigureAwait(false);
                
                actual1 = await this.Repository.Read(id1).ConfigureAwait(false);
                Assert.Null(actual1);
                id1 = null;

                actual2 = await this.Repository.Read(id2).ConfigureAwait(false);
                Assert.Null(actual2);
                id2 = null;
            }
            finally
            {
                if (id1 != null)
                {
                    await this.Repository.Delete(id1).ConfigureAwait(false);
                }
                if (id2 != null)
                {
                    await this.Repository.Delete(id2).ConfigureAwait(false);
                }
                this.Cleanup();
            }
        }
    }
}
