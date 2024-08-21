using InventoryManagementSystemADO.NET.Model;
using InventoryManagementSystemADO.NET.Repository;
using InventoryManagementSystemADO.NET.Service;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryManagementSystemADO.NET
{
    public class InventoryApp
    {
        static async Task Main(string[] args)
        {
            IInventoryService inventoryService = new InventoryServiceImplementation(new InventoryRepositoryImplementation());

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Inventory Management System");
                Console.WriteLine("1. Add Products");
                Console.WriteLine("2. Update products");
                Console.WriteLine("3. Search products by Code");
                Console.WriteLine("4. List All products");
                Console.WriteLine("5. Delete products");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddProducts(inventoryService);
                        break;
                    case "2":
                        await UpdateProducts(inventoryService);
                        break;
                    case "3":
                        await ViewProductsByCode(inventoryService);
                        break;
                    case "4":
                        await ViewAllProducts(inventoryService);
                        break;
                    case "5":
                        await DeleteProducts(inventoryService);
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

        #region AddProducts
        private static async Task AddProducts(IInventoryService inventoryService)
        {
            Inventory inventory = new Inventory();

            // Product Code
            while (true)
            {
                Console.WriteLine("Enter Product Code:");
                inventory.ProductCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inventory.ProductCode))
                {
                    Console.WriteLine("Invalid input for Product Code. Please enter a valid numeric code.");
                }
                else
                {
                    break;
                }
            }

            // Product Name
            while (true)
            {
                Console.WriteLine("Enter Product Name:");
                inventory.ProductName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(inventory.ProductName) && Regex.IsMatch(inventory.ProductName, @"^[a-zA-Z\s]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for Product Name. Please ensure the name contains only letters and spaces.");
                }
            }

            // Category
            while (true)
            {
                Console.WriteLine("Enter Category:");
                inventory.Category = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(inventory.Category) && Regex.IsMatch(inventory.Category, @"^[a-zA-Z\s]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for Category. Please ensure the name contains only letters and spaces.");
                }
            }

            // Quantity
            while (true)
            {
                Console.WriteLine("Enter Quantity:");
                inventory.Quantity = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inventory.Quantity) || !int.TryParse(inventory.Quantity, out _))
                {
                    Console.WriteLine("Invalid input for Quantity. Please enter a valid number.");
                }
                else
                {
                    break;
                }
            }

            // Unit Price
            while (true)
            {
                Console.WriteLine("Enter Unit Price:");
                inventory.UnitPrice = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inventory.UnitPrice) || !decimal.TryParse(inventory.UnitPrice, out _))
                {
                    Console.WriteLine("Invalid input for Unit Price. Please enter a valid number.");
                }
                else
                {
                    break;
                }
            }

            try
            {
                await inventoryService.AddInventoryAsync(inventory);
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add product: {ex.Message}");
            }
        }
        #endregion

        #region UpdateProducts
        private static async Task UpdateProducts(IInventoryService inventoryService)
        {
            Inventory inventory = new Inventory();

            // Ensure product code is provided
            Console.WriteLine("Enter Product Code:");
            inventory.ProductCode = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inventory.ProductCode))
            {
                Console.WriteLine("Product Code is required.");
                return; // Exit the method if Product Code is not provided
            }

            // Product Name
            while (true)
            {
                Console.WriteLine("Enter Product Name (leave empty to keep current):");
                string productName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(productName))
                {
                    if (Regex.IsMatch(productName, @"^[a-zA-Z\s]+$"))
                    {
                        inventory.ProductName = productName;
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Product Name. Please ensure the name contains only letters and spaces.");
                    }
                }
                else
                {
                    break; // No input, keep current value and exit loop
                }
            }

            // Category
            while (true)
            {
                Console.WriteLine("Enter Category (leave empty to keep current):");
                string category = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(category))
                {
                    if (Regex.IsMatch(category, @"^[a-zA-Z\s]+$"))
                    {
                        inventory.Category = category;
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Category. Please ensure the name contains only letters and spaces.");
                    }
                }
                else
                {
                    break; // No input, keep current value and exit loop
                }
            }

            // Quantity
            while (true)
            {
                Console.WriteLine("Enter Quantity (leave empty to keep current):");
                string quantity = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(quantity))
                {
                    if (int.TryParse(quantity, out _))
                    {
                        inventory.Quantity = quantity;
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Quantity. Please enter a valid number.");
                    }
                }
                else
                {
                    break; // No input, keep current value and exit loop
                }
            }

            // Unit Price
            while (true)
            {
                Console.WriteLine("Enter Unit Price (leave empty to keep current):");
                string unitPrice = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(unitPrice))
                {
                    if (decimal.TryParse(unitPrice, out _))
                    {
                        inventory.UnitPrice = unitPrice;
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Unit Price. Please enter a valid number.");
                    }
                }
                else
                {
                    break; // No input, keep current value and exit loop
                }
            }

            try
            {
                await inventoryService.UpdateInventoryAsync(inventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update product: {ex.Message}");
            }
        }
        #endregion

        #region DeleteProducts
        private static async Task DeleteProducts(IInventoryService inventoryService)
        {
            Console.WriteLine("Enter Product Code to Delete:");
            string code = Console.ReadLine();

            try
            {
                await inventoryService.DeleteInventoryAsync(code);
                Console.WriteLine("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete product: {ex.Message}");
            }
        }
        #endregion

        #region ViewAllProducts
        private static async Task ViewAllProducts(IInventoryService inventoryService)
        {
            try
            {
                List<Inventory> inventories = await inventoryService.AllInventoryAsync();
                foreach (var product in inventories)
                {
                    Console.WriteLine($"ProductCode: {product.ProductCode}, Product Name: {product.ProductName}, Category: {product.Category}, Quantity: {product.Quantity}, Unit Price: {product.UnitPrice}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve products: {ex.Message}");
            }
        }
        #endregion

        #region ViewProductsByCode
        private static async Task ViewProductsByCode(IInventoryService inventoryService)
        {
            Console.WriteLine("Enter Product Code to View:");
            string code = Console.ReadLine();

            try
            {
                Inventory inventory = await inventoryService.GetInventoryByCodeAsync(code);
                if (inventory != null)
                {
                    Console.WriteLine($"ProductCode: {inventory.ProductCode}, Product Name: {inventory.ProductName}, Category: {inventory.Category}, Quantity: {inventory.Quantity}, Unit Price: {inventory.UnitPrice}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve product: {ex.Message}");
            }
        }
        #endregion
    }
}
