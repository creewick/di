using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class MyWordParser : IWordParser
    {
        private readonly string filename;

        public MyWordParser(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(filename);
        }
    }
}
