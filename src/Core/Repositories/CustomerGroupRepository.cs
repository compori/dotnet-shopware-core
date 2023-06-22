using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class CustomerGroupRepository<TEntity> : EntityRepository<TEntity> where TEntity : CustomerGroup
    {
        public CustomerGroupRepository(Client client) : base(client)
        {
        }
    }

    public class CustomerGroupRepository : CustomerGroupRepository<CustomerGroup>
    {
        public CustomerGroupRepository(Client client) : base(client)
        {
        }
    }
}