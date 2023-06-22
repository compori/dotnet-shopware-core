using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Compori.Shopware.Factories
{
    public class SettingsFactory : ISettingsFactory
    {
        /// <summary>
        /// Liefert die Einstellungen zrück.
        /// </summary>
        /// <value>The settings.</value>
        public Settings Settings { get; protected set; }

        /// <summary>
        /// Liest die Einstellungen aus einer Json Text Datei.
        /// </summary>
        /// <param name="path">The path.</param>
        public void ReadFromJsonFile(string path)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(path, nameof(path));

            var json = File.ReadAllText(path, Encoding.UTF8);
            this.ReadFromJson(json);
        }

        /// <summary>
        /// Liest die Einstellungen aus einer Json Text Datei.
        /// </summary>
        /// <param name="path">The path.</param>
        public void SaveJsonFile(string path, Settings settings)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(path, nameof(path));
            Guard.AssertArgumentIsNotNull(settings, nameof(settings));

            File.WriteAllText(path, JsonConvert.SerializeObject(settings), Encoding.UTF8);
        }

        /// <summary>
        /// Liest die Einstellungen aus einem Json String
        /// </summary>
        /// <param name="json">The json.</param>
        public void ReadFromJson(string json)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(json, nameof(json));

            this.Settings = JsonConvert.DeserializeObject<Settings>(json);
        }

        /// <summary>
        /// Erstellt die benötigten Einstellungen für den Shopware Client.
        /// </summary>
        /// <returns>Settings.</returns>
        /// <exception cref="System.InvalidOperationException">Die Einstellungen sind nicht gesetzt.</exception>
        public Settings Create()
        {
            if(this.Settings == null)
            {
                throw new InvalidOperationException("Die Einstellungen sind nicht gesetzt.");
            }
            return this.Settings;
        }
    }
}
