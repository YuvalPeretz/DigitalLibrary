using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Models.Classes.Common
{
    public class BookBorrowed
    {
        public string BookName { get; set; }
        public DateTime BookLendDate { get; set; }
        public BookBorrowed(string BookName)
        {
            this.BookName = BookName;
            this.BookLendDate = DateTime.Now;
        }

        public BookBorrowed(string BookName, DateTime BookLendDate)
        {
            this.BookName = BookName;
            this.BookLendDate = BookLendDate;
        }
    }
}
