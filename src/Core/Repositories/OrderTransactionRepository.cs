using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderTransactionRepository<TEntity> : EntityRepository<TEntity> where TEntity : OrderTransaction
    {
        public OrderTransactionRepository(Client client) : base(client)
        {
        }
    }

    public class OrderTransactionRepository : OrderTransactionRepository<OrderTransaction>
    {
        public OrderTransactionRepository(Client client) : base(client)
        {
        }
    }
}
