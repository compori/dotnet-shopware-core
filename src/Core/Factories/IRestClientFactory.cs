using RestSharp;

namespace Compori.Shopware.Factories
{
    public interface IRestClientFactory
    {
        /// <summary>
        /// Erstellt einen RestClient mit den angegebenen Einstellungen.
        /// </summary>
        /// <param name="settings">Die Einstellungen.</param>
        /// <returns>RestClient.</returns>
        RestClient Create(Settings settings);

        /// <summary>
        /// Erstellt einen RestClient mit den angegebenen Einstellungen.
        /// </summary>
        /// <param name="settings">Die Einstellungen.</param>
        /// <param name="token">Optional ein Autentifikationstoken.</param>
        /// <returns>RestClient.</returns>
        RestClient Create(Settings settings, string token);
    }
}
