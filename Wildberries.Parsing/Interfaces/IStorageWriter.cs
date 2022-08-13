using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wildberries.Parsing.Interfaces
{
    /// <summary>
    /// Saves <see cref="SiteItem"/> to any kinds of storage.
    /// </summary>
    public interface IStorageWriter
    {
        /// <summary>
        /// Saves items to storage.
        /// </summary>
        /// <param name="items">Items to be saved.</param>
        /// <exception cref="ArgumentNullException">Throws if items is null.</exception>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task Write(IAsyncEnumerable<SiteItem> items);

        /// <summary>
        /// Creates new page with set name for the following items.
        /// </summary>
        /// <param name="name">Page name.</param>
        /// <exception cref="ArgumentNullException">Throws if name is null.</exception>
        public void CreatePage(string name);

        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task Save();
    }
}
