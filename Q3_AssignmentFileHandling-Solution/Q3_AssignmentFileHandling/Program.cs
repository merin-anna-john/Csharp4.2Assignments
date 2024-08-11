using System.Text.RegularExpressions;

namespace Q3_AssignmentFileHandling
{

    public class Program
    {
        static void Main(string[] args)
        {
            // Directory to save the curriculum vitae
            string directoryPath = "sampleCV"; 

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Get user details
           //Candidate Name validation
            string name;
            while (true)
            {
                Console.WriteLine("Enter Candidate Name:");
                name = Console.ReadLine();
                if (System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Name. It must contain only letters and spaces. Please enter again.");
                }
            }

            //Candidate Phone Number validation
            string phoneNumber;
            while (true)
            {
                Console.WriteLine("Enter your Phone Number (10 digits):");
                phoneNumber = Console.ReadLine();

                // Validate phone number (must contain exactly 10 digits)
                if (IsValidPhoneNumber(phoneNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid phone number. Please enter exactly 10 digits.");
                }
            }
            // Method to validate phone number
            static bool IsValidPhoneNumber(string phoneNumber)
            {
                // Regular expression to check if phone number contains exactly 10 digits
                string pattern = @"^\d{10}$";
                return Regex.IsMatch(phoneNumber, pattern);
            }

            //Candidate Location validation
            string location;
            while (true)
            {
                Console.WriteLine("Enter Candidate Location:");
                location = Console.ReadLine();
                if (System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Location. It must contain only letters and spaces. Please enter again.");
                }
            }


            //Candidate Address validation
            string address;
            while (true)
            {
                Console.WriteLine("Enter Candidate Address:");
                address = Console.ReadLine();
                if (System.Text.RegularExpressions.Regex.IsMatch(address, @"^[a-zA-Z\s]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Address. It must contain only letters and spaces. Please enter again.");
                }
            }

            // Concatenate Name and Location to create the filename
            string fileName = $"{name}_{location}.txt";
            string filePath = Path.Combine(directoryPath, fileName);

            // Write the CV content to the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Curriculum Vitae");
                writer.WriteLine("----------------");
                writer.WriteLine($"Name: {name}");
                writer.WriteLine($"Phone Number: {phoneNumber}");
                writer.WriteLine($"Location: {location}");
                writer.WriteLine($"Address: {address}");
            }

            Console.WriteLine($"Curriculum Vitae saved successfully as {fileName} in {directoryPath}");
        }
    }

}
