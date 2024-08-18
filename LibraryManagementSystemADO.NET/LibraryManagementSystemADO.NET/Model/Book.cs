using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemADO.NET.Model
{
    public class Book
    {
        //Auto-generated fields
        private int Id;

        //Properties
        public string BookCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string BookPrice { get; set; }

        //Default Constructor
        public Book()
        {
            
        }


        //Parameterized Constructor
        public Book(string bookCode, string bookName, string author, string genre, string bookPrice)
        {
            this.BookCode = bookCode;
            this.BookName = bookName;
            this.Author = author;
            this.Genre = genre;
            this.BookPrice = bookPrice;
        }
    }
}
