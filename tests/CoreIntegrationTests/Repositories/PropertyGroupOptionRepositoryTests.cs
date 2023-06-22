using Compori.Shopware.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class PropertyGroupOptionRepositoryTests : BaseTest
    {
        protected PropertyGroupOptionRepository Repository { get; set; }

        protected PropertyGroup PropertyGroup { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new PropertyGroupOptionRepository(this.TestContext.CreateClient());
        }

        private async Task CreateGroup()
        {
            this.PropertyGroup = null;
            var groupRepository = new PropertyGroupRepository(this.TestContext.CreateClient());
            var name = "PropGroup " + Guid.NewGuid().ToString();
            var group = new PropertyGroup
            {
                Name = name,
                DisplayType = "select",
                SortingType = "alphanumeric",
                Filterable = false,
                VisibleOnProductDetailPage = false,
            };
            var id = await groupRepository.Create(group).ConfigureAwait(false);
            Assert.NotNull(id);
            group.Id = id;
            this.PropertyGroup = group;
        }

        private async Task RemoveGroup()
        {
            if (this.PropertyGroup == null)
            {
                return;
            }

            var groupRepository = new PropertyGroupRepository(this.TestContext.CreateClient());
            await groupRepository.Delete(this.PropertyGroup.Id).ConfigureAwait(false);
            this.PropertyGroup = null;
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
                await this.CreateGroup();

                var name = "PropGroupOption " + Guid.NewGuid().ToString();
                var groupOpt = new PropertyGroupOption
                {
                    Name = name,
                    GroupId = this.PropertyGroup.Id
                };
                id = await this.Repository.Create(groupOpt).ConfigureAwait(false);
                Assert.NotNull(id);
                groupOpt.Id = id;

                var actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Equal(groupOpt.Name, actual.Name);
                Assert.Equal(groupOpt.Id, actual.Id);

                groupOpt.Name = "Renamed GroupOption " + Guid.NewGuid().ToString();
                await this.Repository.Update(groupOpt).ConfigureAwait(false);

                actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.Equal(groupOpt.Name, actual.Name);
                Assert.Equal(groupOpt.Id, actual.Id);

                await this.Repository.Delete(groupOpt.Id).ConfigureAwait(false);
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

                await this.RemoveGroup();
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
                await this.CreateGroup();

                var name1 = "PropGroupOption " + Guid.NewGuid().ToString();
                var groupOpt1 = new PropertyGroupOption
                {
                    Name = name1,
                    GroupId = this.PropertyGroup.Id
                };
                var name2 = "PropGroupOption " + Guid.NewGuid().ToString();
                var groupOpt2 = new PropertyGroupOption
                {
                    Id = id2,
                    Name = name2,
                    GroupId = this.PropertyGroup.Id
                };

                await this.Repository.Update(new List<PropertyGroupOption> { groupOpt1, groupOpt2 }).ConfigureAwait(false);
                id1 = groupOpt1.Id;
                var actual1 = await this.Repository.Read(id1).ConfigureAwait(false);
                Assert.Equal(groupOpt1.Name, actual1.Name);
                Assert.Equal(groupOpt1.Id, actual1.Id);

                var actual2 = await this.Repository.Read(id2).ConfigureAwait(false);
                Assert.Equal(groupOpt2.Name, actual2.Name);
                Assert.Equal(groupOpt2.Id, actual2.Id);

                var options = await this.Repository.ReadAllByPropertyGroupId(this.PropertyGroup.Id).ConfigureAwait(false);
                Assert.Equal(2, options.Count);
                Assert.Contains(groupOpt1.Id, options.Select(v => v.Key));
                Assert.Contains(groupOpt2.Id, options.Select(v => v.Key));


                groupOpt1.Name = "Renamed GroupOption " + Guid.NewGuid().ToString();
                await this.Repository.Update(groupOpt1).ConfigureAwait(false);

                actual1 = await this.Repository.Read(id1).ConfigureAwait(false);
                Assert.Equal(groupOpt1.Name, actual1.Name);
                Assert.Equal(groupOpt1.Id, actual1.Id);

                await this.Repository.Delete(new List<string> { groupOpt1.Id, groupOpt2.Id }).ConfigureAwait(false);

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

                await this.RemoveGroup();
                this.Cleanup();
            }
        }
    }
}
