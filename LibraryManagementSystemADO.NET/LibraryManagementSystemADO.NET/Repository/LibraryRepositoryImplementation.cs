using LibraryManagementSystemADO.NET.Model;
using SqlServerConnectionLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemADO.NET.Repository
{
    public class LibraryRepositoryImplementation : ILibraryRepository
    {
        // Retrieve Connection String from App.Config
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        //INSERT
        public async Task AddBookAsync(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "INSERT INTO Library (BookCode,BookName,Author,Genre,BookPrice) " +
                        "VALUES(@BokCode, @BokName, @Aut, @Gen, @BokPri)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@BokCode", book.BookCode);
                        command.Parameters.AddWithValue("@BokName", book.BookName);
                        command.Parameters.AddWithValue("@Aut", book.Author);
                        command.Parameters.AddWithValue("@Gen", book.Genre);
                        command.Parameters.AddWithValue("@BokPri", book.BookPrice);

                        await command.ExecuteNonQueryAsync();
                    }
                }
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

        //Retrieve All Books
        public async Task<List<Book>> AllBookAsync()
        {
            List<Book> books = new List<Book>();

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT BookCode,BookName,Author,Genre,BookPrice FROM Library";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            while (await reader.ReadAsync())
                            {
                                books.Add(new Book
                                {
                                    BookCode = reader["BookCode"].ToString(),
                                    BookName = reader["BookName"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    BookPrice = reader["BookPrice"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return books;
        }

        //Delete Books
        public async Task DeleteBookAsync(string BookCode)
        {
            if (string.IsNullOrEmpty(BookCode)) throw new ArgumentException("Invalid book code", nameof(BookCode));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "DELETE FROM Library WHERE BookCode = @BokCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@BokCode", BookCode);
                        await command.ExecuteNonQueryAsync();
                    }
                }
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
        // Get Book by Code
        public async Task<Book> GetBookByCodeAsync(string BookCode)
        {
            if (string.IsNullOrEmpty(BookCode)) throw new ArgumentException("Invalid book code", nameof(BookCode));

            Book book = null;

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT BookCode,BookName,Author,Genre,BookPrice FROM Library WHERE BookCode = @BokCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@BokCode", BookCode);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                book = new Book
                                {
                                    BookCode = reader["BookCode"].ToString(),
                                    BookName = reader["BookName"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    BookPrice = reader["BookPrice"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return book;
        }

        //Update Book
        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {


                    // Retrieve existing book data
                    Book existingBook = null;
                    string query = "SELECT BookName,Author,Genre,BookPrice FROM Library WHERE BookCode = @BokCode";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@BokCode", book.BookCode);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                existingBook = new Book
                                {
                                    BookName = reader["BookName"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    BookPrice = reader["BookPrice"].ToString()
                                };
                            }
                        }
                    }

                    if (existingBook == null)
                    {
                        Console.WriteLine("Book not found.");
                        return;
                    }

                    // Use existing values if no new input is provided
                    string newBookName = string.IsNullOrWhiteSpace(book.BookName) ? existingBook.BookName : book.BookName;
                    string newAuthor = string.IsNullOrWhiteSpace(book.Author) ? existingBook.Author : book.Author;
                    string newGenre = string.IsNullOrWhiteSpace(book.Genre) ? existingBook.Genre : book.Genre;
                    string newBookPrice = string.IsNullOrWhiteSpace(book.BookPrice) ? existingBook.BookPrice : book.BookPrice;

                    // Update patient data
                    string newQuery = "UPDATE Library SET BookName = @BokName,Author = @Aut, Genre = @Gen, BookPrice = @BokPri WHERE BookCode = @BokCode";
                    using (SqlCommand updateCommand = new SqlCommand(newQuery, conn))
                    {
                        updateCommand.Parameters.AddWithValue("@BokCode", book.BookCode);
                        updateCommand.Parameters.AddWithValue("@BokName", newBookName);
                        updateCommand.Parameters.AddWithValue("@Aut", newAuthor);
                        updateCommand.Parameters.AddWithValue("@Gen", newGenre ?? (object)DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@BokPri", newBookPrice ?? (object)DBNull.Value);

                        int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Book updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update book: {ex.Message}");
            }
        }
    }
}
