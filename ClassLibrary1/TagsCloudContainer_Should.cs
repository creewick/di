using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace TagsCloudContainer
{
    class TagsCloudContainer_Should
    {
        private IWordParser wordParser;
        private IFrequencyProvider frequencyProvider;
        private ICloudBuilder cloudBuilder;
        private IWordFilter[] wordFilters;
        private IWordTransformation[] wordTransformations;

        [SetUp]
        public void SetUp()
        {
            wordParser = Substitute.For<IWordParser>();
            wordFilters = new[] {Substitute.For<IWordFilter>()};
            wordTransformations = new[] { Substitute.For<IWordTransformation>()};
            cloudBuilder = Substitute.For<ICloudBuilder>();
            frequencyProvider = Substitute.For<IFrequencyProvider>();
        }

        [Test]
        public void Draw_ShouldCallOnce_FrequencyProviderGetFrequency()
        {
            frequencyProvider
                .GetFrequency(wordParser, wordFilters, wordTransformations)
                .Returns(new Dictionary<string, int>());
            var container = new TagsCloudContainer(wordParser, wordFilters, wordTransformations, frequencyProvider, cloudBuilder);
            var image = new Bitmap(100, 100);
            container.Draw(Graphics.FromImage(image));

            frequencyProvider.Received(1).GetFrequency(wordParser, wordFilters, wordTransformations);
        }
    }
}
