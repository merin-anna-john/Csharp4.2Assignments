namespace EmployeeAppMultiThreading.Presentation_Layer
{
    //Entry point
    internal class EmployeeApp
    {
        static async Task Main(string[] args)
        {
            await MainMenu.EmployeeMenuAsync();
            Console.ReadKey();
        }
    }
}
