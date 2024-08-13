using ConsoleAppJSONEmployeeApp.BussinessLayer;
using ConsoleAppJSONEmployeeApp.DataAccessLayer;
using ConsoleAppJSONEmployeeApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJSONEmployeeApp.PresentationLayer
{
    public class MainMenu
    {
        //field
        private readonly IEmployeeService _employeeService;

        //constructor
        public MainMenu(IEmployeeService employeeService)
        {
            _employeeService= employeeService;
        }
        //Global
        public static List<Model.Employee> employees = new List<Model.Employee>();

        public static void EmployeeMenu()
        {
            //Menu driven
            bool exit = false;
            var mainMenu=new MainMenu(new EmployeeServiceImplementation(new EmployeeRepositoryImplementation()));

            //Loading employees
            employees = mainMenu._employeeService.ListAllEmployeeService();
            while (!exit)
            {
                Console.WriteLine("\nEmployee Menu");
                Console.WriteLine("-------------");
                Console.WriteLine("1.Add Employee");
                Console.WriteLine("2.List All Employees");
                Console.WriteLine("3.Exit");
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1"://Add Employee
                        //Pass List as parameter and call Add()
                        AddEmployee(employees);//in presentation layer
                        mainMenu._employeeService.SaveEmployeesService(employees);
                        Console.WriteLine("Record added successfully");
                        break;

                    case "2"://List Employee
                        ListEmployees(employees);
                        break;

                    case "3"://Exit
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
        private static void AddEmployee(List<Employee> employees)
        {
            try
            {
                //Get input from user
                Console.WriteLine("Enter employee name: ");
                string name=Console.ReadLine();

                Console.Write("Enter employee age: ");
                int age = 0;
                while(!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.Write("\nInvalid age format .Please enter a valid integer value");
                }
                Console.Write("Enter employee city: ");
                string city=Console.ReadLine();

                //Adding employee to the list
                employees.Add(new Employee { Name = name, Age = age, City = city });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString);
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
