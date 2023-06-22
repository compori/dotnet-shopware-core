namespace Compori.Shopware.Types
{
    public enum TotalCountMode
    {
        /// <summary>
        /// No total is determined.
        /// </summary>
        None = 0,

        /// <summary>
        /// An exact total is determined.
        /// </summary>
        Exact = 1,

        /// <summary>
        ///  It is determined whether there is a next page.
        /// </summary>
        Next = 2,
    }
}
