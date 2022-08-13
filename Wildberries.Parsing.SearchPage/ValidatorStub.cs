using System;
using Wildberries.Parsing.Interfaces;

namespace Wildberries.Parsing.SearchPage
{
    /// <inheritdoc/>
    public class ValidatorStub : ISiteItemValidator
    {
        /// <inheritdoc/>
        public Tuple<bool, string> ValidateBrand(string brand)
        {
            return new Tuple<bool, string>(true, null);
        }

        /// <inheritdoc/>
        public Tuple<bool, string> ValidateFeedbacks(int feedbacks)
        {
            return new Tuple<bool, string>(true, null);
        }

        /// <inheritdoc/>
        public Tuple<bool, string> ValidateId(int id)
        {
            return new Tuple<bool, string>(true, null);
        }

        /// <inheritdoc/>
        public Tuple<bool, string> ValidatePrice(decimal price)
        {
            return new Tuple<bool, string>(true, null);
        }

        /// <inheritdoc/>
        public Tuple<bool, string> ValidateTitle(string title)
        {
            return new Tuple<bool, string>(true, null);
        }
    }
}
