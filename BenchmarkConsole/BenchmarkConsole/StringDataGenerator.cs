using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkConsole
{
    public class StringDataGenerator
    {
        string[] letters = new string[26] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
            "v", "w", "x", "y", "z" };
        List<string> list;
     
        public async Task GenerateWords(int numberOfStringToGenerate, int minWordLen = 1, int maxWordLen = 5, string? fileToWrite = null) { 
            list = new List<string>();
            Random r = new Random();
            while (list.Count < numberOfStringToGenerate)
            {
                StringBuilder sb = new StringBuilder();
                for (int wordLen = 0; wordLen < r.Next(minWordLen, maxWordLen); wordLen++)
                {
                    string letter = letters[r.Next(0, 26)];
                    if (r.Next(0, 2) == 1)
                    {
                        letter = letter.ToUpper();
                    }
                    sb.Append(letter);
                }
                string createdWord = sb.ToString();
                if (list.Contains(createdWord) ) 
                {
                    continue;
                }
                list.Add(createdWord);
            }
        
            if (fileToWrite != null)
            {
                System.IO.File.WriteAllLines(fileToWrite, list);
            }
        }

        

    }
}
