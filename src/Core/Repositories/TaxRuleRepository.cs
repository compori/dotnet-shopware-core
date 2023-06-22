using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class TaxRuleRepository<TEntity> : EntityRepository<TEntity> where TEntity : TaxRule
    {
        public TaxRuleRepository(Client client) : base(client)
        {
        }
    }

    public class TaxRuleRepository : TaxRuleRepository<TaxRule>
    {
        public TaxRuleRepository(Client client) : base(client)
        {
        }
    }
}
