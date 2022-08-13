namespace Wildberries.Parsing.SearchPage.Interfaces
{
    /// <summary>
    /// Gets <see cref="SiteItem"/> from respecting Html page.
    /// </summary>
    public interface IHtmlParser
    {
        /// <summary>
        /// Gets item from Html page.
        /// </summary>
        /// <param name="page">Html page content.</param>
        /// <returns>Filled item.</returns>
        public SiteItem Parse(string page);
    }
}
