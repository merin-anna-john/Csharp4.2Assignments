using OrderManagementSystemADO.NET.Model;
using SqlServerConnectionLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystemADO.NET.Repository
{
    public class OrderRepositoryImplementation : IOrderRepository
    {
        // Retrieve Connection String from App.Config
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        // Insert
        public async Task AddOrderAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "INSERT INTO orders (ProductCode, CustomerName, Quantity, TotalPrice) " +
                        "VALUES(@ProCode, @CustName, @Quant, @TotPri)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", order.ProductCode);
                        command.Parameters.AddWithValue("@CustName", order.CustomerName);
                        command.Parameters.AddWithValue("@Quant", order.Quantity);
                        command.Parameters.AddWithValue("@TotPri", order.TotalPrice);

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
        public async Task<List<Order>> AllOrdersAsync()
        {
            List<Order> orders = new List<Order>();

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT ProductCode, CustomerName, Quantity, TotalPrice FROM orders";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                orders.Add(new Order
                                {
                                    ProductCode = reader["ProductCode"].ToString(),
                                    CustomerName = reader["CustomerName"].ToString(),
                                    Quantity = reader["Quantity"].ToString(),
                                    TotalPrice = reader["TotalPrice"].ToString(),
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

            return orders;
        }

        //Delete Orders
        public async Task DeleteOrderAsync(string productCode)
        {
            if (string.IsNullOrEmpty(productCode)) throw new ArgumentException("Invalid health code", nameof(productCode));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "DELETE FROM orders WHERE ProductCode = @ProCode";

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

        //Get Order by Code
        public async Task<Order> GetOrderByCodeAsync(string productCode)
        {
            if (string.IsNullOrEmpty(productCode)) throw new ArgumentException("Invalid product code", nameof(productCode));

            Order order = null;

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT CustomerName, Quantity, TotalPrice FROM orders WHERE ProductCode = @ProCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", productCode);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                order = new Order
                                {
                                    CustomerName = reader["CustomerName"].ToString(),
                                    Quantity = reader["Quantity"].ToString(),
                                    TotalPrice = reader["TotalPrice"].ToString(),
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

            return order;
        }


        //Update Order
        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {


                    // Retrieve existing order data
                    Order existingOrder = null;
                    string query = "SELECT CustomerName, Quantity, TotalPrice FROM orders WHERE ProductCode = @ProCode";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProCode", order.ProductCode);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                existingOrder = new Order
                                {
                                    CustomerName = reader["CustomerName"].ToString(),
                                    Quantity = reader["Quantity"].ToString(),
                                    TotalPrice = reader["TotalPrice"].ToString()
                                };
                            }
                        }
                    }

                    if (existingOrder == null)
                    {
                        Console.WriteLine("Order not found.");
                        return;
                    }

                    // Use existing values if no new input is provided
                    string newCustomerName = string.IsNullOrWhiteSpace(order.CustomerName) ? existingOrder.CustomerName : order.CustomerName;
                    string newQuantity = string.IsNullOrWhiteSpace(order.Quantity) ? existingOrder.Quantity : order.Quantity;
                    string newTotalPrice = string.IsNullOrWhiteSpace(order.TotalPrice) ? existingOrder.TotalPrice: order.TotalPrice;

                    // Update order data
                    string newQuery = "UPDATE orders SET CustomerName = @CustName, Quantity = @Quant, TotalPrice = @TotPri WHERE ProductCode = @ProCode";
                    using (SqlCommand updateCommand = new SqlCommand(newQuery, conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ProCode", order.ProductCode);
                        updateCommand.Parameters.AddWithValue("@CustName", newCustomerName);
                        updateCommand.Parameters.AddWithValue("@Quant", newQuantity);
                        updateCommand.Parameters.AddWithValue("@TotPri", newTotalPrice ?? (object)DBNull.Value);

                        int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Orders updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Order not found.");
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
                Console.WriteLine($"Failed to update Order: {ex.Message}");
            }
        }
    }
    
}
