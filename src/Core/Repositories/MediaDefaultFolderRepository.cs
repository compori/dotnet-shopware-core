using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class MediaDefaultFolderRepository<TEntity> : EntityRepository<TEntity> where TEntity : MediaDefaultFolder
    {
        public MediaDefaultFolderRepository(Client client) : base(client)
        {
        }
    }

    public class MediaDefaultFolderRepository : MediaDefaultFolderRepository<MediaDefaultFolder>
    {
        public MediaDefaultFolderRepository(Client client) : base(client)
        {
        }
    }
}
