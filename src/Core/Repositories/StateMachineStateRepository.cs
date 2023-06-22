using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class StateMachineStateRepository<TEntity> : EntityRepository<TEntity> where TEntity : StateMachineState
    {
        public StateMachineStateRepository(Client client) : base(client)
        {
        }
    }

    public class StateMachineStateRepository : StateMachineStateRepository<StateMachineState>
    {
        public StateMachineStateRepository(Client client) : base(client)
        {
        }
    }
}