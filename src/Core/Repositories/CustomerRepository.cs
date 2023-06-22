using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class CustomerRepository<TEntity> : EntityRepository<TEntity> where TEntity : Customer
    {
        public CustomerRepository(Client client) : base(client)
        {
        }
    }

    public class CustomerRepository : CustomerRepository<Customer>
    {
        public CustomerRepository(Client client) : base(client)
        {
        }
    }
}