using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ProductManufacturerRepository<TEntity> : EntityRepository<TEntity> where TEntity : ProductManufacturer
    {
        public ProductManufacturerRepository(Client client) : base(client)
        {
        }
    }

    public class ProductManufacturerRepository : ProductManufacturerRepository<ProductManufacturer>
    {
        public ProductManufacturerRepository(Client client) : base(client)
        {
        }
    }
}