using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class MainCategoryRepository<TEntity> : EntityRepository<TEntity> where TEntity : MainCategory
    {
        public MainCategoryRepository(Client client) : base(client)
        {
        }
    }

    public class MainCategoryRepository : MainCategoryRepository<MainCategory>
    {
        public MainCategoryRepository(Client client) : base(client)
        {
        }
    }
}
