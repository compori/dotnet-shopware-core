using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class StateMachineRepository<TEntity> : EntityRepository<TEntity> where TEntity : StateMachine
    {
        public StateMachineRepository(Client client) : base(client)
        {
        }
    }

    public class StateMachineRepository : StateMachineRepository<StateMachine>
    {
        public StateMachineRepository(Client client) : base(client)
        {
        }
    }
}
