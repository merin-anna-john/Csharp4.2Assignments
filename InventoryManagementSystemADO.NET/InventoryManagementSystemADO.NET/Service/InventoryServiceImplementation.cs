using InventoryManagementSystemADO.NET.Model;
using InventoryManagementSystemADO.NET.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystemADO.NET.Service
{
    public class InventoryServiceImplementation : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        
        //Constructor Injection
        public InventoryServiceImplementation(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task AddInventoryAsync(Inventory inventory)
        {
            try
            {
                await _inventoryRepository.AddInventoryAsync(inventory);
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

        public async Task<List<Inventory>> AllInventoryAsync()
        {
            try
            {
                return await _inventoryRepository.AllInventoryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return new List<Inventory>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Inventory>();
            }
        }

        public async Task DeleteInventoryAsync(string productCode)
        {
            try
            {
                await _inventoryRepository.DeleteInventoryAsync(productCode);
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

        public async Task<Inventory> GetInventoryByCodeAsync(string productCode)
        {
            try
            {
                return await _inventoryRepository.GetInventoryByCodeAsync(productCode);
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

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            try
            {
                await _inventoryRepository.UpdateInventoryAsync(inventory);
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
