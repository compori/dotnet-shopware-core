using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderDeliveryPositionRepository<TEntity> : EntityRepository<TEntity> where TEntity : OrderDeliveryPosition
    {
        public OrderDeliveryPositionRepository(Client client) : base(client)
        {
        }
    }

    public class OrderDeliveryPositionRepository : OrderDeliveryPositionRepository<OrderDeliveryPosition>
    {
        public OrderDeliveryPositionRepository(Client client) : base(client)
        {
        }
    }
}
