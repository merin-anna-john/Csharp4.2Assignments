using ConsoleAppJSONEmployeeApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJSONEmployeeApp.DataAccessLayer
{
    public class EmployeeRepositoryImplementation : IEmployeeRepository
    {
        //Creating employee.json
        private const string jsonFilePath= "employee.json";

        //Existing employee in json
        public List<Employee> LoadEmployees()
        {
            //Deserialize
            string jsonData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<Employee>>(jsonData);
        }

        //Save Employee to json file
        public void SaveEmployees(List<Employee> employees)
        {
            //Saving employee to Json file
            string jsonData = JsonConvert.SerializeObject(employees, Formatting.Indented);

            //what all datas in employees list will be return into the jsonData file
            File.WriteAllText(jsonFilePath, jsonData);
        }
    }
}
