using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class CategoryRepository<TEntity> : EntityRepository<TEntity> where TEntity : Category
    {
        public CategoryRepository(Client client) : base(client)
        {
        }
    }

    public class CategoryRepository : CategoryRepository<Category>
    {
        public CategoryRepository(Client client) : base(client)
        {
        }
    }
}
