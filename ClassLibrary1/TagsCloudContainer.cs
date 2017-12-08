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
        private readonly ICloudBuilder cloudBuilder;


        public TagsCloudContainer(IWordParser wordParser, IWordFilter[] wordFilters, IWordTransformation wordTransformation, ICloudBuilder cloudBuilder)
        {
            this.wordParser = wordParser;
            this.wordFilters = wordFilters
                .Select(filter => filter.GetFilter())
                .ToList();
            this.wordTransformation = wordTransformation.GetTransformation();
            this.cloudBuilder = cloudBuilder;
        }

        public void Draw(Graphics g)
        {
            cloudBuilder.Build(GetFrequency(), g);
        }

        public Dictionary<string, int> GetFrequency()
        {
            return wordParser.GetWords()
                .Where(word => wordFilters.All(filter => filter(word)))
                .Select(word => wordTransformation(word))
                .GroupBy(word => word, StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(e => e.Key, e => e.Count());
        }
    }
}
