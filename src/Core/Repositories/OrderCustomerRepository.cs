using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class OrderCustomerRepository<TEntity> : EntityRepository<TEntity> where TEntity : OrderCustomer
    {
        public OrderCustomerRepository(Client client) : base(client)
        {
        }
    }

    public class OrderCustomerRepository : OrderCustomerRepository<OrderCustomer>
    {
        public OrderCustomerRepository(Client client) : base(client)
        {
        }
    }
}
