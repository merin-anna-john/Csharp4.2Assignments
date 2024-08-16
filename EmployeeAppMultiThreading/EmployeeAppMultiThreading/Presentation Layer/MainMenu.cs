using EmployeeAppMultiThreading.Business_Layer;
using EmployeeAppMultiThreading.Data_Access_Layer;
using EmployeeAppMultiThreading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppMultiThreading.Presentation_Layer
{
    public class MainMenu
    {
        private readonly IEmployeeService _employeeService;

        public MainMenu(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public static List<Employee> employees = new List<Employee>();

        public static async Task EmployeeMenuAsync()
        {
            bool exit = false;
            var mainMenu = new MainMenu(new EmployeeServiceImplementation(new EmployeeRepositoryImplementation()));

            // Loading employees asynchronously
            employees = await mainMenu._employeeService.ListAllEmployeeServiceAsync();

            while (!exit)
            {
                Console.WriteLine("\nEmployee Menu");
                Console.WriteLine("-------------");
                Console.WriteLine("1.Add Employee");
                Console.WriteLine("2.List All Employees");
                Console.WriteLine("3.Exit");
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddEmployeeAsync(employees);
                        await mainMenu._employeeService.SaveEmployeesServiceAsync(employees);
                        Console.WriteLine("Record added successfully");
                        break;

                    case "2":
                        ListEmployees(employees);
                        break;

                    case "3":
                        exit = true;
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
            }
        }

        #region AddEmployee
        private static async Task AddEmployeeAsync(List<Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter employee name: ");
                string name = Console.ReadLine();

                Console.Write("Enter employee age: ");
                int age = 0;
                while (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.Write("\nInvalid age format. Please enter a valid integer value: ");
                }
                Console.Write("Enter employee city: ");
                string city = Console.ReadLine();

                employees.Add(new Employee { Name = name, Age = age, City = city });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region ListEmployees
        private static void ListEmployees(List<Employee> employees)
        {
            Console.WriteLine($"{"Name",-20}|{"Age",-5}|{"City",-15}");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name,-20}|{employee.Age,-5}|{employee.City,-15}");
            }
        }
        #endregion
    }
}
