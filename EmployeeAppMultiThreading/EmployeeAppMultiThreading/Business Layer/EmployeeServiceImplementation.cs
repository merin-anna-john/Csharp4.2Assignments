using EmployeeAppMultiThreading.Data_Access_Layer;
using EmployeeAppMultiThreading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppMultiThreading.Business_Layer
{
    public class EmployeeServiceImplementation : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeServiceImplementation(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> ListAllEmployeeServiceAsync()
        {
            return await _employeeRepository.LoadEmployeesAsync();
        }

        public async Task SaveEmployeesServiceAsync(List<Employee> employees)
        {
            await _employeeRepository.SaveEmployeesAsync(employees);
        }
    }
}
