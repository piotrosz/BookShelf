using System;
using System.ComponentModel;

namespace BookShelf.Model
{
    public class Lending : IDataErrorInfo, IEntity
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Person")
                {
                    if (Person == null)
                        result = "Please enter a person";
                }
                return result;
            }
        }
    }
}
