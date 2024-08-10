using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingSearchwordVowelcount.Model
{
    //Interface to search for a word 
    public interface ISearchOperation
    {
        int Search(string word);
    }
}
