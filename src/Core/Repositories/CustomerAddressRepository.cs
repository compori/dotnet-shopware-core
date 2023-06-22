using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class CustomerAddressRepository<TEntity> : EntityRepository<TEntity> where TEntity : CustomerAddress
    {
        public CustomerAddressRepository(Client client) : base(client)
        {
        }
    }

    public class CustomerAddressRepository : CustomerAddressRepository<CustomerAddress>
    {
        public CustomerAddressRepository(Client client) : base(client)
        {
        }
    }
}
