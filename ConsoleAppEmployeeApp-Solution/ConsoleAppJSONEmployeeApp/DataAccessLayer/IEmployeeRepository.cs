using ConsoleAppJSONEmployeeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJSONEmployeeApp.DataAccessLayer
{
    public interface IEmployeeRepository 
    {
        //Load from JSON file
        List<Employee> LoadEmployees();
        void SaveEmployees(List<Employee> employees);
    }
}
