using System;
using System.Collections.Generic;

namespace Wildberries.Parsing.Interfaces
{
    /// <summary>
    /// Gets all <see cref="SiteItem"/> from set page.
    /// </summary>
    public interface IItemsReader
    {
        /// <summary>
        /// Reads all items from site page.
        /// </summary>
        /// <param name="url">Page where items are located.</param>
        /// <returns>Parsed items from the page.</returns>
        /// <exception cref="ArgumentNullException">url is null.</exception>
        /// <exception cref="ArgumentException">url is not valid or inaccessible.</exception>
        public IAsyncEnumerable<SiteItem> Read(string url);
    }
}
