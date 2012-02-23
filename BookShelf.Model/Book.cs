using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

namespace BookShelf.Model
{
    public class Book : IDataErrorInfo, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleOriginal { get; set; }
        public int Year { get; set; }
        public List<Category> Categories { get; set; }
        public byte[] Image { get; set; }
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
                else if (columnName == "Image")
                {
                    if (Image.Length > 1024 * 100)
                    {
                        result = "Image too large. Max image size is 100KB";
                    }
                }
                return result;
            }
        }

        public string CategoriesCsv
        {
            get
            {
                return Categories == null ?
                    "" : string.Join(", ", Categories.Select(x => x.Name).ToArray());
            }
        }
    }
}
