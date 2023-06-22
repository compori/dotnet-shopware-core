using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class TaxRepository<TEntity> : EntityRepository<TEntity> where TEntity : Tax
    {
        public TaxRepository(Client client) : base(client)
        {
        }
    }

    public class TaxRepository : TaxRepository<Tax>
    {
        public TaxRepository(Client client) : base(client)
        {
        }
    }
}
