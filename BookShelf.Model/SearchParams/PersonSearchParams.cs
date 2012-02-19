using System;

namespace BookShelf.Model.SearchParams
{
    public class PersonSearchParams : SearchParamsBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
