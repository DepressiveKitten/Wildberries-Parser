using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wildberries.Parsing.JsonSearchPage.Deserialization
{
    /// <summary>
    /// Element for Json Deserialization.
    /// </summary>
    public class JsonSearchPageDocument
    {
        /// <summary>
        /// Gets or Sets page data.
        /// </summary>
        [JsonPropertyName("data")]
        public Data PageData { get; set; }

        /// <summary>
        /// Element for Json Deserialization.
        /// </summary>
        public class Data
        {
            /// <summary>
            /// Gets or Sets page data.
            /// </summary>
            [JsonPropertyName("products")]
            public List<Product> Products { get; set; }
        }
    }
}
