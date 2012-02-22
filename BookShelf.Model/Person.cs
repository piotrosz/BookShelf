using System;
using System.ComponentModel;

namespace BookShelf.Model
{
    public class Person : IComparable, IDataErrorInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public int CompareTo(object obj)
        {
            return LastName.CompareTo(((Person)obj).LastName);
        }

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrWhiteSpace(FirstName))
                        result = "Please enter a firstname";
                }
                else if (columnName == "LastName")
                {
                    if (string.IsNullOrWhiteSpace(LastName))
                        result = "Please enter a lastname";
                }
                return result;
            }
        }
    }
}
