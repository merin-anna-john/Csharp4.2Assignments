using LibraryManagementSystemADO.NET.Model;
using LibraryManagementSystemADO.NET.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemADO.NET.Service
{
    public class LibraryServiceImplementation : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;

        //Construction Injection
        public LibraryServiceImplementation(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        public async Task AddBookAsync(Book book)
        {
            try
            {
                await _libraryRepository.AddBookAsync(book);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task<List<Book>> AllBookAsync()
        {
            try
            {
                return await _libraryRepository.AllBookAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Book>();
            }

        }

        public async Task DeleteBookAsync(string BookCode)
        {
            try
            {
                await _libraryRepository.DeleteBookAsync(BookCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task<Book> GetBookByCodeAsync(string BookCode)
        {
            try
            {
                return await _libraryRepository.GetBookByCodeAsync(BookCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                await _libraryRepository.UpdateBookAsync(book);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
