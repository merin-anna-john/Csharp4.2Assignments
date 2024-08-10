/*Retrieve five characters starting from the third character and
display it in a console*/
namespace Q2_AssignmentFileHandling
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "sampletext.txt";
            try
            {
                if (File.Exists(filePath))
                {
                    // Read the content of the file
                    string content = File.ReadAllText(filePath);

                    // Check if the file content has enough characters
                    if (content.Length >= 7) // (starting from the third character requires at least 7 characters)
                    {
                        // Retrieve five characters starting from the third character (index 2)
                        string result = content.Substring(2, 5);
                        Console.WriteLine("Extracted characters: " + result);
                    }
                    else
                    {
                        Console.WriteLine("The file content is too short to extract five characters starting from the third.");
                    }
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}

