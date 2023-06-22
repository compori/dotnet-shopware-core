using System;

namespace Compori.Shopware
{
    /// <summary>
    /// Eine ConnectionException signalisiert, dass es Probleme mit der Verbindung gibt, z.B. Timeout oder unbekannter Host.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class ConnectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConnectionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
