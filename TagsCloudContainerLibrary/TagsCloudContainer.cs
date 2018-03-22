using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class TagsCloudContainer
    {
        private readonly IWordParser wordParser;
        private readonly IWordFilter[] wordFilters;
        private readonly IWordTransformation[] wordTransformations;
        private readonly ICloudBuilder cloudBuilder;
        private IFrequencyProvider frequencyProvider;


        public TagsCloudContainer(IWordParser wordParser, IWordFilter[] wordFilters, IWordTransformation[] wordTransformations, IFrequencyProvider frequencyProvider, ICloudBuilder cloudBuilder)
        {
            this.wordParser = wordParser;
            this.wordFilters = wordFilters;
            this.wordTransformations = wordTransformations;
            this.cloudBuilder = cloudBuilder;
            this.frequencyProvider = frequencyProvider;
        }

        public void Draw(Graphics g)
        {
            cloudBuilder.Build(frequencyProvider
                .GetFrequency(wordParser, wordFilters, wordTransformations),
                g);
        }
    }
}
