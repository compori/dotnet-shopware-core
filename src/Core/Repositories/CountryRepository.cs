using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    /// <summary>
    /// Class CountryRepository.
    /// Implements the <see cref="EntityRepository{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity">The type of the t country entity.</typeparam>
    /// <seealso cref="EntityRepository{TCountry}" />
    public class CountryRepository<TEntity> : EntityRepository<TEntity> where TEntity : Country
    {
        public CountryRepository(Client client) : base(client)
        {
        }
    }

    /// <summary>
    /// Class CountryRepository.
    /// Implements the <see cref="EntityRepository{TCountryEntity}" />
    /// </summary>
    /// <seealso cref="EntityRepository{TEntity}" />
    public class CountryRepository : CountryRepository<Country>
    {
        public CountryRepository(Client client) : base(client)
        {
        }
    }
}
