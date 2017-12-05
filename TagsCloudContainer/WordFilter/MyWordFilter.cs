using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public class MyWordFilter : IWordFilter
    {
        private string[] rejectedWords; 

        public MyWordFilter(string filename)
        {
            if (filename == "")
            {
                rejectedWords = new string[0];
                return;
            }
            rejectedWords = File.ReadAllLines(filename);
        }

        public Predicate<string> GetFilter()
        {
            return s => s.Length > 3 && !rejectedWords.Contains(s);
        }
    }
}
