using ConsoleAppJSONEmployeeApp.DataAccessLayer;
using ConsoleAppJSONEmployeeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleAppJSONEmployeeApp.BussinessLayer
{
    public class EmployeeServiceImplementation : IEmployeeService
    {
        //field/data member
        private readonly IEmployeeRepository _employeeRepository;
        //Constructor
        public EmployeeServiceImplementation(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        //List all employees Service Implementation
        List<Employee> IEmployeeService.ListAllEmployeeService()
        {
            return _employeeRepository.LoadEmployees();
        }


        //Add Employee Service Implementation
        public void SaveEmployeesService(List<Employee> employees)
        {
            //Save as Json in repository after Business rule validation
            //So calling SaveEmployees method in repository DataAccessLayer
            _employeeRepository.SaveEmployees(employees);
        }
    }
}
