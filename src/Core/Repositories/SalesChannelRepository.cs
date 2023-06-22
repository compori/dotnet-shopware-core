using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class SalesChannelRepository<TEntity> : EntityRepository<TEntity> where TEntity : SalesChannel
    {
        public SalesChannelRepository(Client client) : base(client)
        {
        }
    }

    public class SalesChannelRepository : SalesChannelRepository<SalesChannel>
    {
        public SalesChannelRepository(Client client) : base(client)
        {
        }
    }
}
