using OrderManagementSystemADO.NET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystemADO.NET.Service
{
    public interface IOrderService
    {
        //Insert
        Task AddOrderAsync(Order order);

        //Update 
        Task UpdateOrderAsync(Order order);

        //Search
        Task<Order> GetOrderByCodeAsync(string productCode);

        //List all Patients
        Task<List<Order>> AllOrdersAsync();

        //Delete Health
        Task DeleteOrderAsync(string productCode);
    }
}
