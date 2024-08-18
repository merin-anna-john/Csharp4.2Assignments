using OrderManagementSystemADO.NET.Model;
using OrderManagementSystemADO.NET.Repository;
using OrderManagementSystemADO.NET.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderManagementSystemADO.NET
{
    public class OrderApp
    {
        static async Task Main(string[] args)
        {
            // Create an instance of Service
            IOrderService orderService = new OrderServiceImplementation(new OrderRepositoryImplementation());

            // Menu Driven
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Order Management System");
                Console.WriteLine("1. Add Orders");
                Console.WriteLine("2. Update Orders");
                Console.WriteLine("3. Search Order by Code");
                Console.WriteLine("4. List All Orders");
                Console.WriteLine("5. Delete Orders");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddOrder(orderService);
                        break;
                    case "2":
                        await UpdateOrder(orderService);
                        break;
                    case "3":
                        await ViewOrderByCode(orderService);
                        break;
                    case "4":
                        await ViewAllOrder(orderService);
                        break;
                    case "5":
                        await DeleteOrder(orderService);
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

        #region AddOrder
        private static async Task AddOrder(IOrderService orderService)
        {
            Order order = new Order();

            //Patient Code
            while (true)
            {
                Console.WriteLine("Enter Product Code:");
                order.ProductCode = Console.ReadLine();

                // Validate Patient Code
                if (string.IsNullOrWhiteSpace(order.ProductCode))
                {
                    Console.WriteLine("Invalid input for Product Code. Please enter a valid numeric code.");
                }
                else
                {
                    break;
                }
            }

            //Customer Name
            while (true)
            {
                Console.WriteLine("Enter Customer Name:");
                order.CustomerName = Console.ReadLine();

                // Validation for CustomerName
                if (!string.IsNullOrWhiteSpace(order.CustomerName) && Regex.IsMatch(order.CustomerName, @"^[a-zA-Z\s]+$"))
                {
                    break; // Valid input, exit loop
                }
                else
                {
                    Console.WriteLine("Invalid input for Customer Name. Please ensure the name contains only letters and spaces.");
                }
            }

            //Quantity
            while (true)
            {
                Console.WriteLine("Enter Quantity:");
                order.Quantity = Console.ReadLine();

                // Validate Patient Age
                if (string.IsNullOrWhiteSpace(order.Quantity) || !int.TryParse(order.Quantity, out _))
                {
                    Console.WriteLine("Invalid input for Quantity. Please enter a valid number.");
                }
                else
                {
                    break; // Valid input, exit loop
                }
            }

            //Total Price
            while (true)
            {
                Console.WriteLine("Enter Total Price:");
                order.TotalPrice = Console.ReadLine();

                // Validate Patient Age
                if (string.IsNullOrWhiteSpace(order.TotalPrice) || !decimal.TryParse(order.TotalPrice, out _))
                {
                    Console.WriteLine("Invalid input for Total Price. Please enter a valid number.");
                }
                else
                {
                    break; // Valid input, exit loop
                }
            }

            try
            {
                await orderService.AddOrderAsync(order);
                Console.WriteLine("Order added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add order: {ex.Message}");
            }
        }
        #endregion


        #region UpdateOrder
        private static async Task UpdateOrder(IOrderService orderService)
        {
            Order order = new Order();

            // Ensure Product Code is provided
            Console.WriteLine("Enter Product Code:");
            order.ProductCode = Console.ReadLine();

            if (string.IsNullOrEmpty(order.ProductCode))
            {
                Console.WriteLine("Product Code is required.");
            }

            else
            {
                //Customer name
                while (true)
                {
                    Console.WriteLine("Enter Customer Name (leave empty to keep current):");
                    order.CustomerName = Console.ReadLine();

                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(order.CustomerName))
                    {
                        break; // Exit loop without changing the current name
                    }

                    // Validation for Customer Name
                    if (Regex.IsMatch(order.CustomerName, @"^[a-zA-Z\s]+$"))
                    {
                        order.CustomerName = order.CustomerName; // Update the customer name
                        break; // Valid input, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Customer Name. Please ensure the name contains only letters and spaces.");
                    }
                }

                //Quantity
                while (true)
                {
                    Console.WriteLine("Enter Quantity (leave empty to keep current):");
                    order.Quantity = Console.ReadLine();

                    // Validate Quantity
                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(order.Quantity))
                    {
                        break; // Exit loop without changing the current 
                    }

                    // Validation for Quantity
                    if (!int.TryParse(order.Quantity, out _))
                    {
                        Console.WriteLine("Invalid input for Quantity. Please enter a valid number.");
                    }
                    else
                    {
                        order.Quantity = order.Quantity; // Update the quantity
                        break; // Valid input, exit loop
                    }


                }


                //Total Price
                while (true)
                {
                    Console.WriteLine("Enter Total Price (leave empty to keep current):");
                    order.TotalPrice = Console.ReadLine();

                    
                    // If the input is empty, keep the current value
                    if (string.IsNullOrWhiteSpace(order.TotalPrice))
                    {
                        break; // Exit loop without changing the current 
                    }

                    // Validation for total price
                    if (!decimal.TryParse(order.TotalPrice, out _))
                    {
                        Console.WriteLine("Invalid input for Total Price. Please enter a valid number.");
                    }
                    else
                    {
                        order.TotalPrice = order.TotalPrice; // Update the total price
                        break; // Valid input, exit loop
                    }


                }

                try
                {
                    await orderService.UpdateOrderAsync(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to update order: {ex.Message}");
                }
            }

        }

        #endregion

        #region DeleteOrder
        private static async Task DeleteOrder(IOrderService orderService)
        {
            Console.WriteLine("Enter Product Code to Delete:");
            string code = Console.ReadLine();

            try
            {
                await orderService.DeleteOrderAsync(code);
                Console.WriteLine("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete order: {ex.Message}");
            }
        }
        #endregion

        #region ViewAllOrders
        private static async Task ViewAllOrder(IOrderService orderService)
        {
            try
            {
                List<Order> orders = await orderService.AllOrdersAsync();
                foreach (var order in orders)
                {
                    Console.WriteLine($"ProductCode: {order.ProductCode},Customer Name: {order.CustomerName}, Quantity: {order.Quantity}, Total Price: {order.TotalPrice}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve orders: {ex.Message}");
            }
        }
        #endregion


        #region ViewOrderByCode
        private static async Task ViewOrderByCode(IOrderService orderService)
        {
            Console.WriteLine("Enter Product Code to View:");
            string code = Console.ReadLine();

            try
            {
                Order order = await orderService.GetOrderByCodeAsync(code);
                if (order != null)
                {
                    Console.WriteLine($"ProductCode: {order.ProductCode},Customer Name: {order.CustomerName}, Quantity: {order.Quantity}, Total Price: {order.TotalPrice}");
                }
                else
                {
                    Console.WriteLine("Order not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve order: {ex.Message}");
            }
        }
        #endregion
    }
}
