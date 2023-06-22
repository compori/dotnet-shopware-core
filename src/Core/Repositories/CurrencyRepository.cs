using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class CurrencyRepository<TEntity> : EntityRepository<TEntity> where TEntity : Currency
    {
        public CurrencyRepository(Client client) : base(client)
        {
        }
    }

    public class CurrencyRepository : CurrencyRepository<Currency>
    {
        public CurrencyRepository(Client client) : base(client)
        {
        }
    }
}
