using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ProductCrossSellingRepository<TEntity> : EntityRepository<TEntity> where TEntity : ProductCrossSelling
    {
        public ProductCrossSellingRepository(Client client) : base(client)
        {
        }
    }

    public class ProductCrossSellingRepository : ProductCrossSellingRepository<ProductCrossSelling>
    {
        public ProductCrossSellingRepository(Client client) : base(client)
        {
        }
    }
}