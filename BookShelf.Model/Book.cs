using System;
using System.Collections.Generic;
using System.Drawing;

namespace BookShelf.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleOriginal { get; set; }
        public int Year { get; set; }
        public List<Category> Categories { get; set; }
        public Bitmap Image { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public Publisher Publisher { get; set; }
        public Author Author { get; set; }
        public Lending LentTo { get; set; }
    }
}
