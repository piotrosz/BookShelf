using System;
using System.ComponentModel;

namespace BookShelf.Model
{
    public class Publisher : IDataErrorInfo, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Name")
                {
                    if (string.IsNullOrWhiteSpace(Name))
                        result = "Please enter a Name";
                }
                return result;
            }
        }
    }
}
