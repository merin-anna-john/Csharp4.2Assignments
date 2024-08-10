using FileHandlingSearchwordVowelcount.Model;

namespace FileHandlingSearchwordVowelcount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "sampletext.txt";
            FileOperation fileOp = new FileOperation(filePath);

            // Search for a word
            Console.WriteLine("Enter a word to be searched: ");
            string wordToSearch = Console.ReadLine();
            int occurrences = fileOp.Search(wordToSearch);
            Console.WriteLine($"The word '{wordToSearch}' occurs {occurrences} times.");

            // Get frequency of each vowel
            Dictionary<char, int> vowelFreq = fileOp.VowelCount();
            Console.WriteLine("Vowel frequencies:");
            foreach (var item in vowelFreq)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
