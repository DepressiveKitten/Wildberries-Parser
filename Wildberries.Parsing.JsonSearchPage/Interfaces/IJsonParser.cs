using System.Collections.Generic;

namespace Wildberries.Parsing.JsonSearchPage.Interfaces
{
    /// <summary>
    /// Parse Json document into <see cref="SiteItem"/>
    /// </summary>
    public interface IJsonParser
    {
        /// <summary>
        /// Parse Json document into <see cref="SiteItem"/>
        /// </summary>
        /// <param name="input">Stream with Json document.</param>
        /// <returns>Site items</returns>
        public IEnumerable<SiteItem> Parse(string input);
    }
}
