using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderDeliveryRepository<TEntity> : EntityRepository<TEntity> where TEntity : OrderDelivery
    {
        public OrderDeliveryRepository(Client client) : base(client)
        {
        }
    }

    public class OrderDeliveryRepository : OrderDeliveryRepository<OrderDelivery>
    {
        public OrderDeliveryRepository(Client client) : base(client)
        {
        }
    }
}
