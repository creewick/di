using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagsCloudContainer
    {
        private readonly IWordParser wordParser;
        private readonly List<Predicate<string>> wordFilters;
        private readonly Func<string, string> wordTransformation;
        public Dictionary<string, int> Words;
        private ICloudBuilder cloudBuilder;
        private readonly string inputFilename;


        public TagsCloudContainer(IWordParser wordParser, IWordFilter[] wordFilters, IWordTransformation wordTransformation, ICloudBuilder cloudBuilder, string inputFilename)
        {
            this.wordParser = wordParser;
            this.wordFilters = wordFilters
                .Select(filter => filter.GetFilter())
                .ToList();
            this.wordTransformation = wordTransformation.GetTransformation();
            this.cloudBuilder = cloudBuilder;
            this.inputFilename = inputFilename;
        }

        public void Draw(Graphics g)
        {
            var words = wordParser.GetWords(inputFilename)
                .Where(word => wordFilters.All(filter => filter(word)))
                .Select(word => wordTransformation(word));
            Words = GetWordsFrequency(words);
            cloudBuilder.Build(Words, g);
        }
        
        private static Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word, StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(e => e.Key, e => e.Count());
        }
    }
}
