using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class StateMachineHistoryRepository<TEntity> : EntityRepository<TEntity> where TEntity : StateMachineHistory
    {
        public StateMachineHistoryRepository(Client client) : base(client)
        {
        }
    }

    public class StateMachineHistoryRepository : StateMachineHistoryRepository<StateMachineHistory>
    {
        public StateMachineHistoryRepository(Client client) : base(client)
        {
        }
    }
}