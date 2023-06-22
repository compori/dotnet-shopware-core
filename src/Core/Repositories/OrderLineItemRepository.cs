using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderLineItemRepository<TEntity> : EntityRepository<TEntity> where TEntity : OrderLineItem
    {
        public OrderLineItemRepository(Client client) : base(client)
        {
        }
    }

    public class OrderLineItemRepository : OrderLineItemRepository<OrderLineItem>
    {
        public OrderLineItemRepository(Client client) : base(client)
        {
        }
    }
}
