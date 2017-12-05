using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class MyWordParser : IWordParser
    {
        public IEnumerable<string> GetWords(string filename)
        {
            return File.ReadAllLines(filename);
        }
    }
}
