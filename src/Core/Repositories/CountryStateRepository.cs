using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    /// <summary>
    /// Class CountryStateRepository.
    /// Implements the <see cref="EntityRepository{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity">The type of the t country state.</typeparam>
    /// <seealso cref="EntityRepository{TEntity}" />
    public class CountryStateRepository<TEntity> : EntityRepository<TEntity> where TEntity : CountryState
    {
        public CountryStateRepository(Client client) : base(client)
        {
        }
    }

    public class CountryStateRepository : CountryStateRepository<CountryState>
    {
        public CountryStateRepository(Client client) : base(client)
        {
        }
    }
}
