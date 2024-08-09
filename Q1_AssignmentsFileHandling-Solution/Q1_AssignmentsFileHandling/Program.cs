/*Append a text to the existing text in a file. Other files should 
 * be able to interact with this file only in the Read Mode while 
 * your application is interacting with the file.
 */
namespace Q1_AssignmentsFileHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Writing and Reading data into the text file
            try
            {
                // Specify file name that is existing
                string fileName = "demoone.txt";

                // Create FileStream with FileShare.Read to allow read access by other processes
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    // Create StreamWriter
                    using (StreamWriter sWriter = new StreamWriter(fs))
                    {
                        sWriter.WriteLine("This is a text that is appended to the file");
                        Console.WriteLine("Text appended to the file successfully.");
                    }
                }

                // Reading the file
                try
                {
                    // Check if file exists
                    if (File.Exists(fileName))
                    {
                        //'using' statement ensures that the FileStream object is disposed of
                        //as soon as the block of code inside the using statement is exited, even if an exception occurs.
                        using (FileStream fstream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        using (StreamReader sReader = new StreamReader(fstream))
                        {
                            Console.WriteLine("Reading data from file....\n");
                            string data = sReader.ReadToEnd();
                            Console.WriteLine(data);
                            Console.WriteLine("File reading is successful.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not found.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during reading: " + e.Message);
                }
            }
            catch (FileNotFoundException fne)
            {
                Console.WriteLine("File not found: " + fne.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Thank you.");
            }
        }
    }
}
