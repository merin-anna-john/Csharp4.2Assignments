using ConsoleAppJSONEmployeeApp.PresentationLayer;

namespace ConsoleAppJSONEmployeeApp
{
    //Entry point
    internal class EmployeeApp
    {
        static void Main(string[] args)
        {
            PresentationLayer.MainMenu.EmployeeMenu();
            Console.ReadKey();
        }
    }
}
