using System.Threading.Tasks;

namespace Wildberries.Parsing.Interfaces
{
    /// <summary>
    /// Saves Data from site to storage.
    /// </summary>
    public interface IParsingService
    {
        /// <summary>
        /// Saves Data from site to storage.
        /// </summary>
        /// <returns>Task.</returns>
        public Task Parse();
    }
}