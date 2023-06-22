using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class PluginRepository<TEntity> : EntityRepository<TEntity> where TEntity : Plugin
    {
        public PluginRepository(Client client) : base(client)
        {
        }
    }

    public class PluginRepository : PluginRepository<Plugin>
    {
        public PluginRepository(Client client) : base(client)
        {
        }
    }
}
