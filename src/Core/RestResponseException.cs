using RestSharp;
using System;

namespace Compori.Shopware
{
    /// <summary>
    /// A RestResponseException occures if anything with the reponse is invalid, e.g. invalid json.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class RestResponseException : Exception
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public RestResponse Response { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public RestResponseException(string message, Exception innerException, RestResponse response) : base(message, innerException)
        {
            this.Response = response;
        }
    }
}
