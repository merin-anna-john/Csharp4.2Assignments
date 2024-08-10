using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingSearchwordVowelcount.Model
{
    public class FileOperation : ISearchOperation, IVowelsOperation
    {
        private readonly string filePath;

        public FileOperation(string filePath)
        {
            this.filePath = filePath;
        }

        // Implementing the search() method to count occurrences of a word
        public int Search(string word)
        {
            int count = 0;
            string content;

            // Reading file content
            if (File.Exists(filePath))
            {
                content = File.ReadAllText(filePath);
                string[] words = content.Split(new[] { ' ', '\n', '\r', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string w in words)
                {
                    if (w.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            return count;
        }

        // Implementing the vowelcount() method to get the frequency of each vowel
        public Dictionary<char, int> VowelCount()
        {
            Dictionary<char, int> vowelFrequency = new Dictionary<char, int>()
        {
            { 'a', 0 }, { 'e', 0 }, { 'i', 0 }, { 'o', 0 }, { 'u', 0 }
        };

            string content;

            // Reading file content
            if (File.Exists(filePath))
            {
                content = File.ReadAllText(filePath).ToLower();

                foreach (char ch in content)
                {
                    if (vowelFrequency.ContainsKey(ch))
                    {
                        vowelFrequency[ch]++;
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            return vowelFrequency;
        }
    }
}
