using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderRepository<TEntity> : EntityRepository<TEntity> where TEntity : Order
    {
        public OrderRepository(Client client) : base(client)
        {
        }
    }

    public class OrderRepository : OrderRepository<Order>
    {
        public OrderRepository(Client client) : base(client)
        {
        }
    }
}