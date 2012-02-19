using System;

namespace BookShelf.Model
{
    public class Lending
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
