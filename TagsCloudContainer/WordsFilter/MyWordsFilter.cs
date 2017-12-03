using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public class MyWordsFilter : IWordsFilter
    {
        private string[] rejectedWords; 

        public MyWordsFilter(string filename)
        {
            if (filename == "")
            {
                rejectedWords = new string[0];
                return;
            }
            if (!File.Exists(filename))
                throw new ArgumentException();
            rejectedWords = File.ReadAllLines(filename);
        }

        public IEnumerable<string> GetFilteredWords(IEnumerable<string> words)
        {
            return words.Where(w => w.Length > 3 && !rejectedWords.Contains(w));
        }
    }
}
