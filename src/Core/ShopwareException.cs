using Compori.Shopware.Types;
using System;

namespace Compori.Shopware
{
    /// <summary>
    /// Eine Site Exception wird durch den Remote Site bzw. den Shop ausgelöst.
    /// Damit wird der Ursprung des Fehler "auf der anderen" Seite signalisiert.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class ShopwareException : Exception
    {
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public Error[] Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopwareException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ShopwareException(string message) : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopwareException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public ShopwareException(string message, Error[] errors) : base(message)
        {
            this.Errors = errors;
        }
    }
}
