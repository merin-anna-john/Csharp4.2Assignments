using System;
using System.IO;
using System.Text.RegularExpressions;
using Q4_AssignmentFileHandling.Model;

namespace Q4_AssignmentFileHandling
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Array to store 10 products
            Product[] products = new Product[10];

            // Collect product details from the user
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"Enter details for product {i + 1}:");

                int productId;
                while (true)
                {
                    Console.Write("Product Id (integer): ");
                    if (int.TryParse(Console.ReadLine(), out productId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Product Id. It must be an integer. Please enter again.");
                    }
                }

                string productName;
                while (true)
                {
                    Console.Write("Product Name: ");
                    productName = Console.ReadLine();
                    if (IsValidName(productName))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Product Name. It must contain only letters and spaces. Please enter again.");
                    }
                }

                decimal productPrice;
                while (true)
                {
                    Console.Write("Product Price (decimal): ");
                    if (decimal.TryParse(Console.ReadLine(), out productPrice) && productPrice > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Product Price. It must be a positive decimal number. Please enter again.");
                    }
                }

                int quantity;
                while (true)
                {
                    Console.Write("Quantity (integer): ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Quantity. It must be a non-negative integer. Please enter again.");
                    }
                }

                products[i] = new Product(productId, productName, productPrice, quantity);
            }

            // Specify the file path
            string filePath = "product.csv";

            // Write products to CSV file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("ProductId,ProductName,ProductPrice,Quantity"); // Header row
                foreach (var product in products)
                {
                    writer.WriteLine($"{product.ProductId},{product.ProductName},{product.ProductPrice},{product.Quantity}");
                }
            }

            Console.WriteLine($"Product details have been saved to {filePath}");
        }

        // Method to validate product name
        static bool IsValidName(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
        }
    }
}
