namespace Compori.Shopware.Helpers
{
    public static class MimeTypeHelper
    {
        /// <summary>
        /// Gets the MIME type by extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        public static string GetMimeTypeByExtension(string extension)
        {
            return !string.IsNullOrWhiteSpace(extension)
                ? MimeTypes.GetMimeType("." + extension)
                : MimeTypes.FallbackMimeType;
        }

        /// <summary>
        /// Gets the name of the MIME type by file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>System.String.</returns>
        public static string GetMimeTypeByFileName(string fileName)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(fileName, nameof(fileName));

            var index = fileName.LastIndexOf(".");
            if (index < 0)
            {
                return MimeTypes.FallbackMimeType;
            }
            return GetMimeTypeByExtension(fileName.Substring(index + 1).ToLowerInvariant());
        }
    }
}
