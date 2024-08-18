using LibraryManagementSystemADO.NET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemADO.NET.Repository
{
    public interface ILibraryRepository
    {
        //Insert
        Task AddBookAsync(Book book);

        //Update 
        Task UpdateBookAsync(Book book);

        //Search
        Task<Book> GetBookByCodeAsync(string BookCode);

        //List all Patients
        Task<List<Book>> AllBookAsync();

        //Delete Health
        Task DeleteBookAsync(string BookCode);
    }
}
