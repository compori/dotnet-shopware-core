using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class AppRepository<TEntity> : EntityRepository<TEntity> where TEntity : App
    {
        public AppRepository(Client client) : base(client)
        {
        }
    }

    public class AppRepository : AppRepository<App>
    {
        public AppRepository(Client client) : base(client)
        {
        }
    }
}
