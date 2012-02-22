using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

namespace BookShelf.Model
{
    public class Book : IDataErrorInfo
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

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Title")
                {
                    if (string.IsNullOrWhiteSpace(Title))
                        result = "Please enter a title";
                }
                else if (columnName == "Author")
                {
                    if (Author == null)
                        result = "Please select an author";
                }

                return result;
            }
        }
    }
}
