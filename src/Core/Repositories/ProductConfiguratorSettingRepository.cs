using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class ProductConfiguratorSettingRepository<TEntity> : EntityRepository<TEntity> where TEntity : ProductConfiguratorSetting
    {
        public ProductConfiguratorSettingRepository(Client client) : base(client)
        {
        }
    }

    public class ProductConfiguratorSettingRepository : ProductConfiguratorSettingRepository<ProductConfiguratorSetting>
    {
        public ProductConfiguratorSettingRepository(Client client) : base(client)
        {
        }
    }
}