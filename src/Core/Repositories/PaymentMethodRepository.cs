using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class PaymentMethodRepository<TEntity> : EntityRepository<TEntity> where TEntity : PaymentMethod
    {
        public PaymentMethodRepository(Client client) : base(client)
        {
        }
    }

    public class PaymentMethodRepository : PaymentMethodRepository<PaymentMethod>
    {
        public PaymentMethodRepository(Client client) : base(client)
        {
        }
    }
}
