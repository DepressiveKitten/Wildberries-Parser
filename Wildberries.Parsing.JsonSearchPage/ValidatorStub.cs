using System;
using Wildberries.Parsing.Interfaces;

namespace Wildberries.Parsing.JsonSearchPage
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
            if (id > 0)
            {
                return new Tuple<bool, string>(true, null);
            }
            return new Tuple<bool, string>(false, "id should be greater then 0");
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
