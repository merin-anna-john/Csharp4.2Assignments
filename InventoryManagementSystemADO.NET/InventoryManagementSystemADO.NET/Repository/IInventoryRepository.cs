using InventoryManagementSystemADO.NET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystemADO.NET.Repository
{
    public interface IInventoryRepository
    {
        //Insert
        Task AddInventoryAsync(Inventory inventory);

        //Update 
        Task UpdateInventoryAsync(Inventory inventory);

        //Search
        Task<Inventory> GetInventoryByCodeAsync(string productCode);

        //List all Inventory
        Task<List<Inventory>> AllInventoryAsync();

        //Delete Inventory
        Task DeleteInventoryAsync(string productCode);
    }
}
