using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class UnitRepository<TEntity> : EntityRepository<TEntity> where TEntity : Unit
    {
        public UnitRepository(Client client) : base(client)
        {
        }
    }

    public class UnitRepository : UnitRepository<Unit>
    {
        public UnitRepository(Client client) : base(client)
        {
        }
    }
}
