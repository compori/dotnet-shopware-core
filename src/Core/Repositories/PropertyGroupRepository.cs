using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class PropertyGroupRepository<TEntity> : EntityRepository<TEntity> where TEntity : PropertyGroup
    {
        public PropertyGroupRepository(Client client) : base(client)
        {
        }
    }

    public class PropertyGroupRepository : PropertyGroupRepository<PropertyGroup>
    {
        public PropertyGroupRepository(Client client) : base(client)
        {
        }
    }
}
