using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PusulaCase
{
    public class LongestVowelSubsequence
    {
        public static string LongestVowelSubsequenceAsJson(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return JsonSerializer.Serialize(new List<object>());
            }
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u',
                                                       'A','E','I','O','U'};
            
            var results = new List<object>();

            for (int i = 0; i < words.Count; i++)
            {
                string word = words[i];
                int maxLen = 0;
                int maxStart = 0;
                int currLen = 0;
                int currStart = 0;

                for (int j = 0; j < word.Length; j++)
                {
                    if (vowels.Contains(word[j]))
                    {
                        if (currLen == 0)
                        {
                            currStart = j;
                        }

                        currLen++;

                        if (currLen > maxLen)
                        {
                            maxLen = currLen;
                            maxStart = currStart;
                        }
                    }
                    else
                    {
                        currLen = 0;
                        currStart = 0;
                    }
                }

                string sequence = (maxLen > 0 && maxStart != 0) ? word.Substring(maxStart, maxLen) : "";

                results.Add(new
                {
                    word = word,
                    sequence = sequence,
                    length = maxLen
                });
            }

            return JsonSerializer.Serialize(results);
        }
    }
}
