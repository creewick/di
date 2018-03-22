using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class WordParser : IWordParser
    {
        private readonly string filename;

        public WordParser(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(filename);
        }
    }
}
