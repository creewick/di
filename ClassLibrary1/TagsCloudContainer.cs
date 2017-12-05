using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagsCloudContainer
    {
        private readonly IWordParser wordParser;
        private readonly Predicate<string> wordFilter;
        private readonly Func<string, string> wordTransformation;
        public Dictionary<string, int> Words;
        private ICloudBuilder cloudBuilder;
        private readonly string inputFilename;


        public TagsCloudContainer(IWordParser wordParser, IWordFilter wordFilter, IWordTransformation wordTransformation, ICloudBuilder cloudBuilder, string inputFilename)
        {
            this.wordParser = wordParser;
            this.wordFilter = wordFilter.GetFilter();
            this.wordTransformation = wordTransformation.GetTransformation();
            this.cloudBuilder = cloudBuilder;
            this.inputFilename = inputFilename;
        }

        public TagsCloudContainer Build()
        {
            var words = wordParser.GetWords(inputFilename)
                .Where(word => wordFilter(word))
                .Select(word => wordTransformation(word));
            Words = GetWordsFrequency(words);
            return this;
        }

        public void SaveAsImage(string filename)
        {
            var image = cloudBuilder.Build(Words);
            image.Save(filename);
        }


        private static Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word, StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(e => e.Key, e => e.Count());
        }
    }
}
