using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ShippingMethodRepository<TEntity, TTaggableWith, TTag> : EntityRepository<TEntity, TTaggableWith, TTag>
        where TEntity : ShippingMethod<TTaggableWith>
        where TTaggableWith : ShippingMethodTag, new()
        where TTag : Tag
    {
        public ShippingMethodRepository(Client client) : base(client)
        {
        }
    }

    public class ShippingMethodRepository : ShippingMethodRepository<ShippingMethod, ShippingMethodTag, Tag>
    {
        public ShippingMethodRepository(Client client) : base(client)
        {
        }
    }
}
