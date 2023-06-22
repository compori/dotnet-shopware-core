using Compori.Shopware.Factories;
using Compori.Shopware.Types;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shopware
{
    public class Client
    {
        /// <summary>
        /// Der Logger
        /// </summary>
        private static readonly NLog.Logger Log = NLog.LogManager.GetLogger(typeof(Client).FullName);

        /// <summary>
        /// Liefert die Factory für das Ermitteln der Einstellungen zurück.
        /// </summary>
        /// <value>Die Factory für das Ermitteln der Einstellungen.</value>
        private ISettingsFactory SettingsFactory { get; }

        /// <summary>
        /// Die Einstellungen.
        /// </summary>
        private Settings settings;

        /// <summary>
        /// Liefert die Einstellungen zurück.
        /// </summary>
        /// <value>Die Einstellungen.</value>
        private Settings Settings => this.settings ??= this.SettingsFactory.Create();

        /// <summary>
        /// Liefert die Factory für das Erstellen von Rest Clients zurück.
        /// </summary>
        /// <value>Die Factory für das Erstellen von Rest Clients.</value>
        private IRestClientFactory RestClientFactory { get; }

        /// <summary>
        /// The access token
        /// </summary>
        private AccessToken _accessToken;

        /// <summary>
        /// The token expiration
        /// </summary>
        private DateTime? _tokenExpiration;

        /// <summary>
        /// Liefert einen Wert zurück, der an gibt ob ein Access Token vorhanden ist oder nicht.
        /// </summary>
        /// <value><c>true</c> wenn ein Access Token verfügbar ist; andernfalls, <c>false</c>.</value>
        private bool IsTokenValid => this._accessToken != null && this._tokenExpiration.HasValue && this._tokenExpiration >= DateTime.UtcNow;


        /// <summary>
        /// Gets the rest client.
        /// </summary>
        private RestClient _restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="settingsFactory">Die Factory für das Ermitteln der Einstellungen.</param>
        /// <param name="restClientFactory">Die Factory für das Erstellen von Rest Clients.</param>
        public Client(
            ISettingsFactory settingsFactory,
            IRestClientFactory restClientFactory)
        {
            this.SettingsFactory = settingsFactory;
            this.RestClientFactory = restClientFactory;
            this._accessToken = null;
            this._tokenExpiration = null;
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <typeparam name="TResponseType">The type of the t response type.</typeparam>
        /// <param name="response">Die generische Restantwort.</param>
        /// <param name="skipNotFoundError">Wenn <c>true</c> wird ein Not Found Fehler übersprungen, andernfalls löst dieser eine SiteException Ausnahme aus.</param>
        /// <returns>TResponseType.</returns>
        private TResponseType ProcessResponse<TResponseType>(RestResponse<TResponseType> response, DateTime startTime, bool skipNotFoundError = false)
        {
            Guard.AssertArgumentIsNotNull(response, nameof(response));

            this.ProcessResponse((RestResponse)response, startTime, skipNotFoundError);

            return response.StatusCode == HttpStatusCode.NotFound
                ? default
                : response.Data;
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="response">Die Restantwort.</param>
        /// <param name="skipNotFoundError">if set to <c>true</c> [skip not found error].</param>
        private void ProcessResponse(RestResponse response, DateTime startTime, bool skipNotFoundError = false)
        {
            this.ProcessResponse(this.Settings, response, startTime, skipNotFoundError);
        }

        /// <summary>
        /// Erstellt für die Restanfrage und -antwort einen Trace.
        /// </summary>
        /// <param name="settings">Die Einstellungen.</param>
        /// <param name="request">Die Restanfrage.</param>
        /// <param name="response">Die Restantwort.</param>
        /// <param name="skipNotFoundError">if set to <c>true</c> [skip not found error].</param>
        /// <exception cref="ConnectionException"></exception>
        /// <exception cref="ShopwareException"></exception>
        private void ProcessResponse(Settings settings, RestResponse response, DateTime startTime, bool skipNotFoundError = false)
        {
            Guard.AssertArgumentIsNotNull(response, nameof(response));

            var trace = settings?.Trace ?? false;
            var duration = DateTime.UtcNow.Subtract(startTime);
            var durationText = $"Request time: {duration:G}.";

            // Ist Tracing für die Anfragen aktiviert?
            if (trace)
            {
                Trace(response);
            }

            //
            // Daten nicht vollständig empfangen.
            //
            if (response.ResponseStatus != ResponseStatus.Completed && !"application/json".Equals(response.ContentType))
            {
                // Sollte das Tracing deaktiviert sein, wird es bei einem Fehler auf jeden Fall durchgeführt.
                if (!trace)
                {
                    Trace(response);
                }

                Log.Warn($"{response.Request.Resource}: {response.Request.Method} request returns '{(int)response.StatusCode} {response.StatusDescription}' status code. {durationText}");

                // Verbindungsproblem
                throw new ConnectionException(
                    !string.IsNullOrWhiteSpace(response.ErrorMessage) ? response.ErrorMessage : (response.ErrorException?.Message ?? response.StatusDescription),
                    response.ErrorException
                );
            }

            // Antwort war erfolgreich.
            if (response.IsSuccessful)
            {
                Log.Trace($"{response.Request.Resource}: {response.Request.Method} request was successful. {durationText}");
                return;
            }

            // Antwort war "nicht gefunden". Soll aber nicht als Fehler behandelt werden.
            if (skipNotFoundError && response.StatusCode == HttpStatusCode.NotFound)
            {
                Log.Trace($"{response.Request.Resource}: {response.Request.Method} request returns '404 Not Found' status code. {durationText}");
                return;
            }

            //
            // Nun ist es zu einem Fehler gekommen.
            // Sollte das Tracing deaktiviert sein, wird es bei einem Fehler auf jeden Fall durchgeführt.
            //
            if (!trace)
            {
                Trace(response);
            }

            // Fehler verarbeiten und Exception schmeißen.
            var errors = GetErrors(response.Content);
            if (errors == null && !string.IsNullOrWhiteSpace(response.ErrorMessage) && response.ErrorException != null && response.IsSuccessStatusCode)
            {
                throw new RestResponseException($"A client connection error occurs. {response.ErrorMessage}", response.ErrorException, response);
            }
            var message = "In Shopware occurs an error.";
            if (errors != null && errors.Length > 0)
            {
                message = string.Join(" ", errors.Select(v => (
                    (!string.IsNullOrWhiteSpace(v.Title) ? v.Title + ". " : "")
                    +
                    (v.Detail ?? "")
                    +
                    (!string.IsNullOrWhiteSpace(v.Source?.Pointer) ? " - Source Poiner: " + v.Source?.Pointer : "")
                ).Trim()));
            }
            Log.Warn($"{response.Request.Method} error on '{response.Request.Resource}' - {message}. {durationText}");

            throw new ShopwareException(message, errors);
        }

        /// <summary>
        /// Liefert die Fehlermeldungen zurück.
        /// </summary>
        /// <returns>Error[].</returns>
        private static Error[] GetErrors(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<ErrorList>(content)?.Errors;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Traces the request.
        /// </summary>
        /// <param name="request">The request.</param>
        private void Trace(RestRequest request)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Trace Request");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine($"Resource:      {request.Resource}");
                sb.AppendLine($"Method:        {request.Method}");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine("Parameters:");
                foreach (var parameter in request.Parameters)
                {
                    var value = "";
                    if (!string.IsNullOrEmpty(parameter.Name))
                    {
                        value += $"Name={parameter.Name},";
                    }
                    if (parameter.Value != null)
                    {
                        value += $"Value={parameter.Value}";
                    }
                    if (parameter.ContentType == ContentType.Json)
                    {
                        value += $"({parameter.ContentType}) " + JsonConvert.SerializeObject(parameter.Value);
                    }
                    value = value.Trim();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    sb.AppendLine($"{value}");

                }
                /*
                if (request.Body != null)
                {
                    sb.AppendLine("------------------------------------------------------------------");
                    sb.AppendLine("Body:");
                    sb.AppendLine($"Name:          {request.Body?.Name ?? "N/A"}");
                    sb.AppendLine($"ContentType:   {request.Body?.ContentType ?? "N/A"}");
                    sb.AppendLine($"Content:       {request.Body?.Value ?? "N/A"}");
                }
                */
                Log.Trace(sb.ToString());
            }
            catch (Exception e)
            {
                // Sicherstellen, dass hier Fehler, nicht den Rest blockieren.
                Log.Error(e);
            }
        }

        /// <summary>
        /// Traces the response.
        /// </summary>
        /// <param name="response">The response.</param>
        private void Trace(RestResponse response)
        {
            this.Trace(response.Request);

            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("Trace Response");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine($"StatusCode:         {response.StatusCode}");
                sb.AppendLine($"StatusDescription:  {response.StatusDescription ?? "N/A"}");
                sb.AppendLine($"ProtocolVersion:    {response.Version?.ToString() ?? "N/A"}");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine("Headers");
                foreach (var header in response.Headers)
                {
                    var value = $"{header.Name ?? ""}";

                    if (!string.IsNullOrWhiteSpace(header.Value?.ToString()))
                    {
                        value += $": {header.Value}";
                    }

                    if (!string.IsNullOrWhiteSpace(header.ContentType))
                    {
                        value += $" (Content-Type {header.ContentType})";
                    }

                    value = value.Trim();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }
                    sb.AppendLine($"{value}");
                }
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine($"ContentEncoding: {string.Join(", ", response.ContentEncoding)}");
                sb.AppendLine($"ContentLength:   {response.ContentLength?.ToString("N0") ?? "N/A"}");
                sb.AppendLine($"ContentType:     {response.ContentType ?? "N/A"}");
                sb.AppendLine($"Content:         {response.Content ?? "N/A"}");
                Log.Trace(sb.ToString());
            }
            catch (Exception e)
            {
                // Sicherstellen, dass hier Fehler, nicht den Rest blockieren.
                Log.Error(e);
            }
        }

        /// <summary>
        /// Erstellt einen Access Token anhand der Einstellungen.
        /// </summary>
        /// <param name="cancelToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>AccessToken.</returns>
        public async Task<AccessToken> CreateAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            using var client = this.RestClientFactory.Create(this.Settings);

            var accessTokenRequest = new AccessTokenRequest
            {
                GrantType = "client_credentials",
                ClientId = this.Settings.ClientId,
                ClientSecret = this.Settings.ClientKey
            };

            Log.Trace("Prepare requesting an access token.");

            var startTime = DateTime.UtcNow;
            var request = new RestRequest("/oauth/token")
                .AddJsonBody(accessTokenRequest);
            var response = await client.ExecutePostAsync<AccessToken>(request, cancellationToken).ConfigureAwait(false);

            var accessToken = this.ProcessResponse(response, startTime);

            Log.Trace("Requesting an access token completed.");

            return accessToken;
        }

        /// <summary>
        /// Erstellt den REST Client sofern nicht bereits vorhanden und konfiguriert ihn.
        /// </summary>
        /// <returns>RestClient.</returns>
        /// <exception cref="RestResponseException">Die Client Einstellungen konnten nicht ermittelt werden.</exception>
        private async Task<RestClient> Create(CancellationToken cancellationToken)
        {
            // Wenn der Access Token gültig ist und der Rest Client vorhanden, liefere den Rest Client zurück.
            if (this._restClient != null && this.IsTokenValid)
            {
                return this._restClient;
            }

            // Ursprünglichen Rest Client aufräumen.
            if (this._restClient != null)
            {
                this._restClient.Dispose();
                this._restClient = null;
            }

            // Erstelle einen neuen Access Token
            Log.Trace("The current access token is invalid. Creating a new access token.");

            this._accessToken = await this.CreateAccessTokenAsync(cancellationToken).ConfigureAwait(false)
                ?? throw new ShopwareException("Der angeforderte Access Token ist leer.");

            this._tokenExpiration = DateTime.UtcNow.AddSeconds(this._accessToken.ExpiresIn - 60);
            Log.Trace($"The new access token ({this._accessToken.TokenType}) was created and will expire on {this._tokenExpiration.Value.ToLocalTime():G}");


            // Erstelle den Client mit Access Token
            this._restClient = this.RestClientFactory.Create(this.Settings, this._accessToken.Value);
            return this._restClient;
        }

        /// <summary>
        /// Sendet eine Anfrage über den REST Client.
        /// </summary>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="request">Das Anfrageobjekt.</param>
        /// <param name="method">Die zu verwendene HTTP Methode.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="skipNotFoundError">Wenn <c>true</c> soll kein Fehler bei 404 nicht gefunden generiert werden.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<TOutput> Execute<TOutput>(RestRequest request, Method method, CancellationToken cancellationToken = default, bool skipNotFoundError = false)
        {
            Guard.AssertArgumentIsNotNull(request, nameof(request));

            var client = await this.Create(cancellationToken);
            var startTime = DateTime.UtcNow;
            var response = await client.ExecuteAsync<TOutput>(request, method, cancellationToken).ConfigureAwait(false);
            return this.ProcessResponse(response, startTime, skipNotFoundError);
        }

        /// <summary>
        /// Sendet eine Anfrage über den REST Client.
        /// </summary>
        /// <param name="request">Das Anfrageobjekt.</param>
        /// <param name="method">Die zu verwendene HTTP Methode.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="skipNotFoundError">Wenn <c>true</c> soll kein Fehler bei 404 nicht gefunden generiert werden.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task Execute(RestRequest request, Method method, CancellationToken cancellationToken = default, bool skipNotFoundError = false)
        {
            Guard.AssertArgumentIsNotNull(request, nameof(request));

            var client = await this.Create(cancellationToken);
            var startTime = DateTime.UtcNow;
            var response = await client.ExecuteAsync(request, method, cancellationToken).ConfigureAwait(false);
            this.ProcessResponse(response, startTime, skipNotFoundError);
        }

        /// <summary>
        /// Sendet eine POST Anfrage über den REST Client.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameter.</typeparam>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenen Daten.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<TOutput> Post<TInput, TOutput>(string uri, TInput data, CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            return await this.Execute<TOutput>(new RestRequest(uri).AddJsonBody(data), Method.Post, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine POST Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameter.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenen Daten.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if request was successful, <c>false</c> otherwise.</returns>
        public async Task Post<TInput>(string uri, TInput data, CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            await this.Execute(new RestRequest(uri).AddJsonBody(data), Method.Post, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine POST Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameter.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenen Daten.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if request was successful, <c>false</c> otherwise.</returns>
        public async Task<TOutput> Post<TOutput>(string uri, CancellationToken cancellationToken = default) where TOutput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));

            return await this.Execute<TOutput>(new RestRequest(uri), Method.Post, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine POST Anfrage über den REST Client und überträgt den Dateiinhalt.
        /// </summary>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="path">Der vollständige Pfad zur Datei.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task PostFile(string uri, string path, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(path, nameof(path));

            await this.PostFile(uri, path, null, null, null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Überträgt eine Datei.
        /// </summary>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="path">Der vollständige Pfad zur Datei.</param>
        /// <param name="name">Der Name, wenn leer wird dieser anhand des Pfad ermittelt.</param>
        /// <param name="fileName">Name der Datei, wenn leer wird dieser anhand des Pfad ermittelt.</param>
        /// <param name="extension">Die Dateierweiterung, wenn leer wird dieser anhand des Pfad ermittelt.</param>
        /// <param name="mimeType">Der Mimetype, wenn leer wird dieser anhand der Dateieweiterung ermittelt.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task PostFile(string uri, string path, string name = null, string fileName = null, string extension = null, string mimeType = null, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNullOrWhiteSpace(path, nameof(path));

            var fileInfo = new FileInfo(path);

            // Name prüfen
            name = string.IsNullOrWhiteSpace(name) ? fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) : name;

            // Dateiname prüfen
            fileName = string.IsNullOrWhiteSpace(fileName) ? fileInfo.Name : fileName;

            // Erweiterung prüfen
            extension = string.IsNullOrWhiteSpace(extension) ? fileInfo.Extension : extension;
            if (extension.StartsWith("."))
            {
                extension = extension.Substring(1);
            }

            var data = File.ReadAllBytes(path);
            await this.PostBytes(uri, data, name, fileName, extension, mimeType, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Überträgt binäre Daten.
        /// </summary>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die binären Daten.</param>
        /// <param name="name">Der Name.</param>
        /// <param name="fileName">Name der Datei.</param>
        /// <param name="extension">Die Dateierweiterung.</param>
        /// <param name="mimeType">Der Mimetype, wenn leer wird dieser anhand der Dateieweiterung ermittelt.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task PostBytes(string uri, byte[] data, string name, string fileName, string extension, string mimeType, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            var request = new RestRequest(uri);
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                request.AddParameter("fileName", fileName, ParameterType.QueryString);
                if (!string.IsNullOrWhiteSpace(extension))
                {
                    request.AddParameter("extension", extension, ParameterType.QueryString);
                }
            }

            // Mimetyp prüfen
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                mimeType = !string.IsNullOrWhiteSpace(extension)
                    ? MimeTypes.GetMimeType("." + extension)
                    : MimeTypes.FallbackMimeType;
            }

            request
                .AddParameter("Content-Type", mimeType, ParameterType.HttpHeader)
                .AddParameter(new BodyParameter(name, data, ContentType.Binary, DataFormat.Binary));

            await this.Execute(request, Method.Post, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine PATCH Anfrage über den REST Client.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameter.</typeparam>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenen Daten.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<TOutput> Patch<TInput, TOutput>(string uri, TInput data, CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            return await this.Execute<TOutput>(new RestRequest(uri).AddJsonBody(data), Method.Patch, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine PATCH Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameter.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenen Daten.</param>
        /// <returns><c>true</c> if request was successful, <c>false</c> otherwise.</returns>
        public async Task Patch<TInput>(string uri, TInput data, CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            await this.Execute(new RestRequest(uri).AddJsonBody(data), Method.Patch, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine DELETE Anfragen an eine URI.
        /// </summary>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task Delete(string uri, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            await this.Execute(new RestRequest(uri), Method.Delete, cancellationToken, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine GET Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<TOutput> Get<TOutput>(string uri, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            return await this.Execute<TOutput>(new RestRequest(uri), Method.Get, cancellationToken, true).ConfigureAwait(false);
        }
    }
}
