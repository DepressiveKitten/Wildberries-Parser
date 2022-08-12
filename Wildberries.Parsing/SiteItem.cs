namespace Wildberries.Parsing
{
    /// <summary>
    /// General information about item from site.
    /// </summary>
    public class SiteItem
    {
        /// <summary>
        /// Gets or Sets Item's Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Item's Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or Sets Item's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Item's Feddbacks.
        /// </summary>
        public int Feedbacks { get; set; }

        /// <summary>
        /// Gets or Sets Item's Price.
        /// </summary>
        public decimal Price { get; set; }
    }
}
