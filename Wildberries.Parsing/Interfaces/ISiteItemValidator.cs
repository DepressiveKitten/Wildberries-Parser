using System;

namespace Wildberries.Parsing.Interfaces
{
    /// <summary>
    /// Interface for site items validation.
    /// </summary>
    public interface ISiteItemValidator
    {
        /// <summary>
        /// Validate all parameters of item.
        /// </summary>
        /// <param name="siteItem">Object to validate.</param>
        /// <exception cref="ArgumentNullException">item is null.</exception>
        /// <exception cref="ArgumentException">One of items parameters is not Valid.</exception>
        public void Validate(SiteItem siteItem)
        {
            if (siteItem is null)
            {
                throw new ArgumentNullException(nameof(siteItem));
            }

            Tuple<bool, string> result;

            result = this.ValidateId(siteItem.Id);
            if (!result.Item1)
            {
                throw new ArgumentException(result.Item2, nameof(siteItem));
            }

            result = this.ValidateTitle(siteItem.Title);
            if (!result.Item1)
            {
                throw new ArgumentException(result.Item2, nameof(siteItem));
            }

            result = this.ValidateBrand(siteItem.Brand);
            if (!result.Item1)
            {
                throw new ArgumentException(result.Item2, nameof(siteItem));
            }

            result = this.ValidateFeedbacks(siteItem.Feedbacks);
            if (!result.Item1)
            {
                throw new ArgumentException(result.Item2, nameof(siteItem));
            }

            result = this.ValidatePrice(siteItem.Price);
            if (!result.Item1)
            {
                throw new ArgumentException(result.Item2, nameof(siteItem));
            }
        }

        /// <summary>
        /// validate Id.
        /// </summary>
        /// <param name="id">Id that should be validating.</param>
        /// <returns>result of validation and error message if validation fails.</returns>
        public Tuple<bool, string> ValidateId(int id);

        /// <summary>
        /// validate Title.
        /// </summary>
        /// <param name="title">Title that should be validating.</param>
        /// <returns>result of validation and error message if validation fails.</returns>
        public Tuple<bool, string> ValidateTitle(string title);

        /// <summary>
        /// validate Brand.
        /// </summary>
        /// <param name="brand">Brand that should be validating.</param>
        /// <returns>result of validation and error message if validation fails.</returns>
        public Tuple<bool, string> ValidateBrand(string brand);

        /// <summary>
        /// validate Feedbacks.
        /// </summary>
        /// <param name="feedbacks">Feedbacks that should be validating.</param>
        /// <returns>result of validation and error message if validation fails.</returns>
        public Tuple<bool, string> ValidateFeedbacks(int feedbacks);

        /// <summary>
        /// validate Price.
        /// </summary>
        /// <param name="price">Price that should be validating.</param>
        /// <returns>result of validation and error message if validation fails.</returns>
        public Tuple<bool, string> ValidatePrice(decimal price);
    }
}
