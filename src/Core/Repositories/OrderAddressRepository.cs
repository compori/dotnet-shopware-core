using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderAddressRepository<TEntity> : EntityRepository<TEntity> where TEntity : OrderAddress
    {
        public OrderAddressRepository(Client client) : base(client)
        {
        }
    }

    public class OrderAddressRepository : OrderAddressRepository<OrderAddress>
    {
        public OrderAddressRepository(Client client) : base(client)
        {
        }
    }
}
