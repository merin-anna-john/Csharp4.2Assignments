using LibraryManagementSystemADO.NET.Model;
using LibraryManagementSystemADO.NET.Repository;
using LibraryManagementSystemADO.NET.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManagementSystemADO.NET
{
    internal class BookApp
    {
        static async Task Main(string[] args)
        {
            // Create an instance of Service
            ILibraryService libraryService = new LibraryServiceImplementation(new LibraryRepositoryImplementation());

            // Menu Driven
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Add Books");
                Console.WriteLine("2. Update Books");
                Console.WriteLine("3. Search Book by Code");
                Console.WriteLine("4. List All Books");
                Console.WriteLine("5. Delete Book");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddBook(libraryService);
                        break;
                    case "2":
                        await UpdateBook(libraryService);
                        break;
                    case "3":
                        await ViewBookByCode(libraryService);
                        break;
                    case "4":
                        await ViewAllBooks(libraryService);
                        break;
                    case "5":
                        await DeleteBook(libraryService);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }
            }
            Console.ReadKey();
        }

        #region AddPatient
        private static async Task AddBook(ILibraryService libraryService)
        {
            Book book=new Book();

            //Book Code
            while (true)
            {
                Console.WriteLine("Enter Book Code:");
                book.BookCode = Console.ReadLine();

                // Validate Book Code
                if (string.IsNullOrWhiteSpace(book.BookCode))
                {
                    Console.WriteLine("Invalid input for Book Code. Please enter a valid numeric code.");
                }
                else
                {
                    break;
                }
            }

            //Book Name
            while (true)
            {
                Console.WriteLine("Enter Book Name:");
                book.BookName = Console.ReadLine();

                // Validation for Book Name
                if (!string.IsNullOrWhiteSpace(book.BookName) && Regex.IsMatch(book.BookName, @"^[a-zA-Z\s]+$"))
                {
                    break; // Valid input, exit loop
                }
                else
                {
                    Console.WriteLine("Invalid input for Book Name. Please ensure the name contains only letters and spaces.");
                }
            }

            //Author
            while (true)
            {
                Console.WriteLine("Enter Book's Author:");
                book.Author = Console.ReadLine();

                // Validate Author
                if (!string.IsNullOrWhiteSpace(book.Author) && Regex.IsMatch(book.Author, @"^[a-zA-Z\s]+$"))
                {
                    break; // Valid input, exit loop
                }
                else
                {
                    Console.WriteLine("Invalid input for Book's Author. It contains only letters and spaces.");
                }
            }

            //Genre
            while (true)
            {
                Console.WriteLine("Enter Book Genre:");
                book.Genre = Console.ReadLine();
                // Validate Author
                if (!string.IsNullOrWhiteSpace(book.Genre) && Regex.IsMatch(book.Genre, @"^[a-zA-Z\s]+$"))
                {
                                        break; // Valid input, exit loop

                }
                else
                {
                    Console.WriteLine("Invalid input for Book Genre. It contains only letters and spaces.");
                }
            }

            //Book Price
            while (true)
            {
                Console.WriteLine("Enter Book Price:");
                book.BookPrice = Console.ReadLine();
                // Validate Patient Age
                if (string.IsNullOrWhiteSpace(book.BookPrice) || !decimal.TryParse(book.BookPrice, out _))
                {
                    Console.WriteLine("Invalid input for Book Price. Please enter a valid number.");
                }
                else
                {
                    break; // Valid input, exit loop
                }
            }

            try
            {
                await libraryService.AddBookAsync(book);
                Console.WriteLine("Book added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add book: {ex.Message}");
            }
        }
        #endregion


        #region UpdateBook
        private static async Task UpdateBook(ILibraryService libraryService)
        {
            Book book = new Book();

            // Ensure BookCode is provided
            Console.WriteLine("Enter Book Code:");
            book.BookCode = Console.ReadLine();

            if (string.IsNullOrEmpty(book.BookCode))
            {
                Console.WriteLine("Book Code is required.");
            }

            else
            {
                //Book name
                while (true)
                {
                    Console.WriteLine("Enter Book Name (leave empty to keep current):");
                    book.BookName = Console.ReadLine();

                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(book.BookName))
                    {
                        break; // Exit loop without changing the current name
                    }

                    // Validation for Book Name
                    if (Regex.IsMatch(book.BookName, @"^[a-zA-Z\s]+$"))
                    {
                        book.BookName = book.BookName; // Update the book name
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Book Name. Please ensure the name contains only letters and spaces.");
                    }
                }

                //Author
                while (true)
                {
                    Console.WriteLine("Enter Book Author (leave empty to keep current):");
                    book.Author  = Console.ReadLine();

                    
                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(book.Author))
                    {
                        break; // Exit loop without changing the current name
                    }

                    // Validation for Book Author
                    if (Regex.IsMatch(book.Author, @"^[a-zA-Z\s]+$"))
                    {
                        book.Author = book.Author; // Update the book name
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Book Author. It contains only letters and spaces.");
                    }
                }

                //Genre
                while (true)
                {
                    Console.WriteLine("Enter Book genre (leave empty to keep current):");
                    book.Genre = Console.ReadLine();

                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(book.Genre))
                    {
                        break; // Exit loop without changing the current name
                    }

                    // Validation for Patient Name
                    if (Regex.IsMatch(book.Genre, @"^[a-zA-Z\s]+$"))
                    {
                        book.Genre = book.Genre; // Update the book name
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Book.Genre. It contains only letters and spaces.");
                    }
                }


                //Book Price
                while (true)
                {
                    Console.WriteLine("Enter Book Price (leave empty to keep current):");
                    book.BookPrice = Console.ReadLine();

                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(book.BookPrice))
                    {
                        break; // Exit loop without changing the current name
                    }

                    // Validation for Patient Name
                    if (!int.TryParse(book.BookPrice, out _))
                    {
                        Console.WriteLine("Invalid input for Book Price. Please enter a valid number.");
                    }
                    else
                    {
                        book.BookPrice = book.BookPrice; // Update the BookPrice
                        break; // Valid input, exit loop
                    }
                }

                try
                {
                    await libraryService.UpdateBookAsync(book);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to update book: {ex.Message}");
                }
            }

        }

        #endregion


        #region ViewBookByCode
        private static async Task ViewBookByCode(ILibraryService libraryService)
        {
            Console.WriteLine("Enter Book Code to View:");
            string code = Console.ReadLine();

            try
            {
                Book book =await libraryService.GetBookByCodeAsync(code); ;
                if (book != null)
                {
                    Console.WriteLine($"Book Code: {book.BookCode}, Book Name: {book.BookName}, Author: {book.Author}, Genre: {book.Genre}, Book Price: {book.BookPrice}");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve book: {ex.Message}");
            }
        }
        #endregion


        #region ViewAllPatients
        private static async Task ViewAllBooks(ILibraryService libraryService)
        {
            try
            {
                List<Book> books = await libraryService.AllBookAsync();
                foreach (var book in books)
                {
                    Console.WriteLine($"Book Code: {book.BookCode}, Book Name: {book.BookName}, Author: {book.Author}, Genre: {book.Genre}, Book Price: {book.BookPrice}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve books: {ex.Message}");
            }
        }
        #endregion


        #region DeleteBook
        private static async Task DeleteBook(ILibraryService libraryService)
        {
            Console.WriteLine("Enter Book Code to Delete:");
            string code = Console.ReadLine();

            try
            {
                await libraryService.DeleteBookAsync(code);
                Console.WriteLine("Book deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete book: {ex.Message}");
            }
        }
        #endregion


    }
}
