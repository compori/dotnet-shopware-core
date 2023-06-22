using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class SalutationRepository<TEntity> : EntityRepository<TEntity> where TEntity : Salutation
    {
        public SalutationRepository(Client client) : base(client)
        {
        }
    }

    public class SalutationRepository : SalutationRepository<Salutation>
    {
        public SalutationRepository(Client client) : base(client)
        {
        }
    }
}
