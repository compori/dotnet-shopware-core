using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ProductVisibilityRepository<TEntity> : EntityRepository<TEntity> where TEntity : ProductVisibility
    {
        public ProductVisibilityRepository(Client client) : base(client)
        {
        }
    }

    public class ProductVisibilityRepository : ProductVisibilityRepository<ProductVisibility>
    {
        public ProductVisibilityRepository(Client client) : base(client)
        {
        }
    }
}