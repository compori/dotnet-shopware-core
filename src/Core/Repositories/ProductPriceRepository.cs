using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ProductPriceRepository<TEntity> : EntityRepository<TEntity> where TEntity : ProductPrice
    {
        public ProductPriceRepository(Client client) : base(client)
        {
        }
    }

    public class ProductPriceRepository : ProductPriceRepository<ProductPrice>
    {
        public ProductPriceRepository(Client client) : base(client)
        {
        }
    }
}