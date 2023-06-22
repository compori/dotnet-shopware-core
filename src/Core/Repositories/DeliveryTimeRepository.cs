using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class DeliveryTimeRepository<TEntity> : EntityRepository<TEntity> where TEntity : DeliveryTime
    {
        public DeliveryTimeRepository(Client client) : base(client)
        {
        }
    }

    public class DeliveryTimeRepository : DeliveryTimeRepository<DeliveryTime>
    {
        public DeliveryTimeRepository(Client client) : base(client)
        {
        }
    }
}
