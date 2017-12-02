using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class MyWordsParser : IWordsParser
    {
        public IEnumerable<string> GetWords(string filename)
        {
            if (!File.Exists(filename))
                throw new ArgumentException();
            return File.ReadAllLines(filename);
        }
    }
}
