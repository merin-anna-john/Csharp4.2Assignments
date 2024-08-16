using EmployeeAppMultiThreading.Model;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace EmployeeAppMultiThreading.Data_Access_Layer
{
    public class EmployeeRepositoryImplementation : IEmployeeRepository
    {
        private const string jsonFilePath = "employeeOne.json";

        public async Task<List<Employee>> LoadEmployeesAsync()
        {
            if (!File.Exists(jsonFilePath))
            {
                // If the file doesn't exist, return an empty list
                return new List<Employee>();
            }

            // Asynchronously read the JSON data
            string jsonData = await File.ReadAllTextAsync(jsonFilePath);
            return JsonConvert.DeserializeObject<List<Employee>>(jsonData);
        }

        public async Task SaveEmployeesAsync(List<Employee> employees)
        {
            // Serialize and asynchronously write to JSON file
            string jsonData = JsonConvert.SerializeObject(employees, Formatting.Indented);
            await File.WriteAllTextAsync(jsonFilePath, jsonData);
        }
    }
}
