using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class TagRepository<TEntity> : EntityRepository<TEntity> where TEntity : Tag
    {
        public TagRepository(Client client) : base(client)
        {
        }
    }

    public class TagRepository : TagRepository<Tag>
    {
        public TagRepository(Client client) : base(client)
        {
        }
    }
}
