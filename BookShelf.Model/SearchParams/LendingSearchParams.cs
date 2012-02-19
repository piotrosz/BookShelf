using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShelf.Model.SearchParams
{
    public class LendingSearchParams : SearchParamsBase
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string LenderFirstName { get; set; }
        public string LenderLastName { get; set; }
    }
}
