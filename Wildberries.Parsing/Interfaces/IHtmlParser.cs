using System;
using System.IO;

namespace Wildberries.Parsing.Interfaces
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
        /// <exception cref="ArgumentNullException">Throws if page is null.</exception>
        /// <exception cref="ArgumentException">page is not valid or misses some parameters.</exception>
        public SiteItem Parse(StreamReader page);
    }
}
