using InventoryManagementSystemADO.NET.Model;
using SqlServerConnectionLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystemADO.NET.Repository
{
    public class InventoryRepositoryImplementation : IInventoryRepository
    {
        // Retrieve Connection String from App.Config
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        // Insert
        public async Task AddInventoryAsync(Inventory inventory)
        {
            if (inventory == null) throw new ArgumentNullException(nameof(inventory));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "INSERT INTO Warehouse (ProductCode, ProductName, Category, Quantity, UnitPrice) " +
                        "VALUES(@ProCode, @ProName,@Cat, @Quant, @UniPri)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", inventory.ProductCode);
                        command.Parameters.AddWithValue("@ProName", inventory.ProductName);
                        command.Parameters.AddWithValue("@Cat", inventory.Category);
                        command.Parameters.AddWithValue("@Quant", inventory.Quantity);
                        command.Parameters.AddWithValue("@UniPri", inventory.UnitPrice);

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

        //Retrieve all Orders

        public async Task<List<Inventory>> AllInventoryAsync()
        {
            List<Inventory> inventory = new List<Inventory>();

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT ProductCode, ProductName, Category, Quantity, UnitPrice FROM Warehouse";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                inventory.Add(new Inventory
                                {
                                    ProductCode = reader["ProductCode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    Quantity = reader["Quantity"].ToString(),
                                    UnitPrice = reader["UnitPrice"].ToString(),
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

            return inventory;
        }

        //Delete Inventory
        public async Task DeleteInventoryAsync(string productCode)
        {
            if (string.IsNullOrEmpty(productCode)) throw new ArgumentException("Invalid inventory code", nameof(productCode));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "DELETE FROM warehouse WHERE ProductCode = @ProCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", productCode);
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

        //Get inventory by code
        public async Task<Inventory> GetInventoryByCodeAsync(string productCode)
        {
            if (string.IsNullOrEmpty(productCode)) throw new ArgumentException("Invalid product code", nameof(productCode));

            Inventory inventory = null;

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT ProductName, Category, Quantity, UnitPrice FROM warehouse WHERE ProductCode = @ProCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", productCode);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                inventory = new Inventory
                                {
                                    ProductName = reader["ProductName"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    Quantity = reader["Quantity"].ToString(),
                                    UnitPrice = reader["UnitPrice"].ToString(),
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

            return inventory;
        }


        // Update Inventory
        public async Task UpdateInventoryAsync(Inventory inventory)
        {

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    // Retrieve existing inventory data
                    Inventory existingInventory = null;
                    string query = "SELECT ProductName, Category, Quantity, UnitPrice FROM Warehouse WHERE ProductCode = @ProCode";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", inventory.ProductCode);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                existingInventory = new Inventory
                                {
                                    ProductName = reader["ProductName"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    Quantity = reader["Quantity"].ToString(),
                                    UnitPrice = reader["UnitPrice"].ToString(),
                                };
                            }
                        }
                    }

                    if (existingInventory == null)
                    {
                        Console.WriteLine("Product not found.");
                        return;
                    }

                    // Use existing values if no new input is provided
                    string newProductName = string.IsNullOrWhiteSpace(inventory.ProductName) ? existingInventory.ProductName : inventory.ProductName;
                    string newCategory = string.IsNullOrWhiteSpace(inventory.Category) ? existingInventory.Category : inventory.Category;
                    string newQuantity = string.IsNullOrWhiteSpace(inventory.Quantity) ? existingInventory.Quantity : inventory.Quantity;
                    string newUnitPrice = string.IsNullOrWhiteSpace(inventory.UnitPrice) ? existingInventory.UnitPrice : inventory.UnitPrice;

                    // Update inventory data
                    string newQuery = "UPDATE Warehouse SET ProductName = @ProName, Category = @Cat, Quantity = @Quant, UnitPrice = @UniPri WHERE ProductCode = @ProCode";
                    using (SqlCommand updateCommand = new SqlCommand(newQuery, conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ProCode", inventory.ProductCode);
                        updateCommand.Parameters.AddWithValue("@ProName", newProductName);
                        updateCommand.Parameters.AddWithValue("@Cat", newCategory);
                        updateCommand.Parameters.AddWithValue("@Quant", newQuantity ?? (object)DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@UniPri", newUnitPrice ?? (object)DBNull.Value);

                        int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Product updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
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
                Console.WriteLine($"Failed to update product: {ex.Message}");
            }
        }
    }
}