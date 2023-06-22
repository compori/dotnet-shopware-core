using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class MediaFolderRepository<TEntity> : EntityRepository<TEntity> where TEntity : MediaFolder
    {
        public MediaFolderRepository(Client client) : base(client)
        {
        }
    }

    public class MediaFolderRepository : MediaFolderRepository<MediaFolder>
    {
        public MediaFolderRepository(Client client) : base(client)
        {
        }
    }
}
