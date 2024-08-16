using EmployeeAppMultiThreading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppMultiThreading.Business_Layer
{
    public interface IEmployeeService
    {
        //List all Employees Service
        Task<List<Employee>>ListAllEmployeeServiceAsync();

        //Add Employees Service
        Task SaveEmployeesServiceAsync(List<Employee> employees);
    }
}
