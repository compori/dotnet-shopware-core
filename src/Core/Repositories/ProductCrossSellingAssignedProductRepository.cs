using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ProductCrossSellingAssignedProductRepository<TEntity> : EntityRepository<TEntity> where TEntity : ProductCrossSellingAssignedProduct
    {
        public ProductCrossSellingAssignedProductRepository(Client client) : base(client)
        {
        }
    }

    public class ProductCrossSellingAssignedProductRepository : ProductCrossSellingAssignedProductRepository<ProductCrossSellingAssignedProduct>
    {
        public ProductCrossSellingAssignedProductRepository(Client client) : base(client)
        {
        }
    }
}