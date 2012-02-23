using System;

namespace BookShelf.Model.SearchParams
{
    public class SearchParamsBase
    {
        public bool IsDefined(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public bool IsDefined(int? value)
        {
            return value.HasValue;
        }

        public bool IsDefined(DateTime? value)
        {
            return value.HasValue;
        }
    }
}
