using OrderManagementSystemADO.NET.Model;
using OrderManagementSystemADO.NET.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystemADO.NET.Service
{
    public class OrderServiceImplementation : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        //Constructor Injection
        public OrderServiceImplementation(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.AddOrderAsync(order);
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

        public async Task<List<Order>> AllOrdersAsync()
        {
            try
            {
                return await _orderRepository.AllOrdersAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return new List<Order>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Order>();
            }
        }

        public async Task DeleteOrderAsync(string productCode)
        {
            try
            {
                await _orderRepository.DeleteOrderAsync(productCode);
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

        public async Task<Order> GetOrderByCodeAsync(string productCode)
        {
            try
            {
                return await _orderRepository.GetOrderByCodeAsync(productCode);
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

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateOrderAsync(order);
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
