using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class StateMachineTransitionRepository<TEntity> : EntityRepository<TEntity> where TEntity : StateMachineTransition
    {
        public StateMachineTransitionRepository(Client client) : base(client)
        {
        }
    }

    public class StateMachineTransitionRepository : StateMachineTransitionRepository<StateMachineTransition>
    {
        public StateMachineTransitionRepository(Client client) : base(client)
        {
        }
    }
}
