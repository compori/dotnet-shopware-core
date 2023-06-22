using Xunit;
using Compori.Shopware.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class CategoryRepositoryTests : BaseTest
    {
        protected CategoryRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new CategoryRepository(this.TestContext.CreateClient());
        }

        [Fact()]
        public async Task TestCreate()
        {
            this.Setup();
            var id = "";
            try
            {
                var categories = await this.Repository.ReadAll(new Types.Search
                {
                    Limit = 100,
                    Filters = new Types.Filter[]
                    {
                        new  Types.Filter {

                            Field = "name", Type = "equals", Value = "Home" }
                    }
                }).ConfigureAwait(false);
                var home = categories.FirstOrDefault(v => "Home".Equals(v.Value.Name)).Value;
                Assert.NotNull(home);
                var myCategory = new Category
                {
                    Active = true,
                    ParentId = home.Id,
                    Name = "Test",
                    Visible = true,
                };
                id = await this.Repository.Create(myCategory);

            }
            finally
            {
                if (!string.IsNullOrEmpty(id))
                {
                    await this.Repository.Delete(id);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestUpdate()
        {
            this.Setup();
            var id = "";
            try
            {
                var categories = await this.Repository.ReadAll(new Types.Search
                {
                    Limit = 100,
                    Filters = new Types.Filter[]
                    {
                        new  Types.Filter {

                            Field = "name", Type = "equals", Value = "Home" }
                    }
                }).ConfigureAwait(false);
                var home = categories.FirstOrDefault(v => "Home".Equals(v.Value.Name)).Value;
                Assert.NotNull(home);
                var parendId = home.Id;
                var expected = new Category
                {
                    Active = true,
                    ParentId = home.Id,
                    Name = "Test",
                    Visible = true,
                };
                id = await this.Repository.Create(expected);
                var actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.NotNull(actual);
                Assert.Equal(id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(parendId, actual.ParentId);

                var update = new Category
                {
                    Id = id,
                    Name = "Test 2"
                };

                await this.Repository.Update(update);

                actual = await this.Repository.Read(id).ConfigureAwait(false);
                Assert.NotNull(actual);
                Assert.Equal(id, actual.Id);
                Assert.Equal(update.Name, actual.Name);
                Assert.Equal(parendId, actual.ParentId);
                Assert.Equal(expected.Active, actual.Active);
                Assert.Equal(expected.Visible, actual.Visible);

            }
            finally
            {
                if (!string.IsNullOrEmpty(id))
                {
                    await this.Repository.Delete(id);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestReadAll()
        {
            this.Setup();
            try
            {
                var categories = await this.Repository.ReadAll(new Types.Search { Limit = 100 }).ConfigureAwait(false);
                Assert.NotNull(categories);
                Assert.Contains(categories, v => "Home".Equals(v.Value.Name));
                Assert.Contains(categories, v => "Kontakt".Equals(v.Value.Name));
            }
            finally
            {
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestReadAllIds()
        {
            this.Setup();
            try
            {
                var categories = await this.Repository.ReadAllIds(new Types.Search { Limit = 100 }).ConfigureAwait(false);
                Assert.NotNull(categories);
            }
            finally
            {
                this.Cleanup();
            }
        }
    }
}