using EmployeeAppMultiThreading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppMultiThreading.Data_Access_Layer
{
    public interface IEmployeeRepository
    {
        //Load from JSON file
        Task<List<Employee>> LoadEmployeesAsync();
        Task SaveEmployeesAsync(List<Employee> employees);
    }
}
