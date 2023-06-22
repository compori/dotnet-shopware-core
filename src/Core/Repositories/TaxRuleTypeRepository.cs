using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class TaxRuleTypeRepository<TEntity> : EntityRepository<TEntity> where TEntity : TaxRuleType
    {
        public TaxRuleTypeRepository(Client client) : base(client)
        {
        }
    }

    public class TaxRuleTypeRepository : TaxRuleTypeRepository<TaxRuleType>
    {
        public TaxRuleTypeRepository(Client client) : base(client)
        {
        }
    }
}
