using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingSearchwordVowelcount.Model
{
    //Interface to count the vowels 
    public interface IVowelsOperation
    {
        Dictionary<char, int> VowelCount();

    }
}
