/*Copy a file to another destination specified by the user.*/
namespace Q5_AssignmentFileHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Prompt the user for the source file path
                Console.Write("Enter the source file path: ");
                string sourceFilePath = Console.ReadLine();

                // Check if the source file exists
                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine("Source file does not exist.");
                    return;
                }

                // Prompt the user for the destination file path
                Console.Write("Enter the destination file path: ");
                string destinationFilePath = Console.ReadLine();

                // Copy the file to the destination path
                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);

                Console.WriteLine("File copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
