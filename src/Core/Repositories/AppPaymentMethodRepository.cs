using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class AppPaymentMethodRepository<TEntity> : EntityRepository<TEntity> where TEntity : AppPaymentMethod
    {
        public AppPaymentMethodRepository(Client client) : base(client)
        {
        }
    }

    public class AppPaymentMethodRepository : AppPaymentMethodRepository<AppPaymentMethod>
    {
        public AppPaymentMethodRepository(Client client) : base(client)
        {
        }
    }

}
