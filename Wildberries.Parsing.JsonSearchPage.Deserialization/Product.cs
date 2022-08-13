using System.Text.Json.Serialization;

namespace Wildberries.Parsing.JsonSearchPage.Deserialization
{
    /// <summary>
    /// Element for Json Deserialization.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or Sets Item's Title.
        /// </summary>
        [JsonPropertyName("name")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Item's Brand.
        /// </summary>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or Sets Item's Id.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Item's Feddbacks.
        /// </summary>
        [JsonPropertyName("feedbacks")]
        public int Feedbacks { get; set; }

        /// <summary>
        /// Gets or Sets Item's Price.
        /// </summary>
        [JsonPropertyName("priceU")]
        public decimal Price { get; set; }
    }
}
