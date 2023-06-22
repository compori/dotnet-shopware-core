using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class AclRoleRepository<TEntity> : EntityRepository<TEntity> where TEntity : AclRole
    {
        public AclRoleRepository(Client client) : base(client)
        {
        }
    }

    public class AclRoleRepository : AclRoleRepository<AclRole>
    {
        public AclRoleRepository(Client client) : base(client)
        {
        }
    }
}
