using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class AppActionButtonRepository<TEntity> : EntityRepository<TEntity> where TEntity : AppActionButton
    {
        public AppActionButtonRepository(Client client) : base(client)
        {
        }
    }

    public class AppActionButtonRepository : AppActionButtonRepository<AppActionButton>
    {
        public AppActionButtonRepository(Client client) : base(client)
        {
        }
    }
}
