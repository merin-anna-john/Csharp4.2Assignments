using ConsoleAppJSONEmployeeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppJSONEmployeeApp.Model;
using ConsoleAppJSONEmployeeApp.DataAccessLayer;

namespace ConsoleAppJSONEmployeeApp.BussinessLayer
{
    public interface IEmployeeService
    {
        //List all Employees Service
        List<Employee> ListAllEmployeeService();

        //Add Employees Service
        void SaveEmployeesService(List<Employee> employees);
    }
}
