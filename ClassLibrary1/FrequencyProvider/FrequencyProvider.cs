using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class FrequencyProvider : IFrequencyProvider
    {
        public Dictionary<string, int> GetFrequency(IWordParser wordParser, IWordFilter[] wordFilters, IWordTransformation[] wordTransformations)
        {
            return wordParser.GetWords()
                .Where(word => wordFilters
                    .All(filter => filter.GetFilter()(word)))
                .Select(word => wordTransformations
                    .Aggregate(word, (current, t) => t.GetTransformation()(current)))
                .GroupBy(word => word, StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(e => e.Key, e => e.Count());
        }
    }
}
