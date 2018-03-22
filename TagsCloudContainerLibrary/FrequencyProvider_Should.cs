using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;


namespace TagsCloudContainer
{
    class FrequencyProvider_Should
    {
        private IWordParser wordParser;
        private IWordFilter wordFilter;
        private IWordTransformation wordTransformation;

        [SetUp]
        public void SetUp()
        {
            wordParser = Substitute.For<IWordParser>();
            wordFilter = Substitute.For<IWordFilter>();
            wordTransformation = Substitute.For<IWordTransformation>();
        }

        [Test]
        public void NoFilterNoTransform_SameWordList()
        {
            var words = new[] {"abc", "def"};
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s);

            new FrequencyProvider().GetFrequency(wordParser, new[] {wordFilter}, new [] {wordTransformation})
                .Keys.Should().BeEquivalentTo(words);
        }

        [Test]
        public void GetFrequency_GroupBy()
        {
            var words = new[] { "a", "a", "a", "c", "c" };
            var expected = new Dictionary<string, int>{ { "a", 3 }, { "c", 2 } };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s);

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter }, new[] { wordTransformation })
                .ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void EmptyWordList()
        {
            var words = new string[0];
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s);

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter }, new[] { wordTransformation })
                .Should().BeEmpty();
        }

        [Test]
        public void Filter_FilteredWordList()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => s == "abc");
            wordTransformation.GetTransformation().Returns(s => s);

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter }, new[] { wordTransformation })
                .Keys.Should().BeEquivalentTo("abc");
        }

        [Test]
        public void FilterWithEmptyResult()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => false);
            wordTransformation.GetTransformation().Returns(s => s);

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter }, new[] { wordTransformation })
                .Keys.Should().BeEmpty();
        }

        [Test]
        public void Transform_TransformedWordList()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s.ToUpper());

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter }, new[] { wordTransformation })
                .Keys.Should().BeEquivalentTo("ABC", "DEF");
        }

        [Test]
        public void FilterAndThenTransform_WordList()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => s == "abc");
            wordTransformation.GetTransformation().Returns(s => s.ToUpper());

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter }, new[] { wordTransformation })
                .Keys.Should().BeEquivalentTo("ABC");
        }

        [Test]
        public void MultipleFilters()
        {
            var words = new[] { "a", "b", "c" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => s != "a");
            var wordFilter2 = Substitute.For<IWordFilter>();
            wordFilter2.GetFilter().Returns(s => s != "b");
            wordTransformation.GetTransformation().Returns(s => s);

            new FrequencyProvider().GetFrequency(wordParser, new[] { wordFilter, wordFilter2 }, new[] { wordTransformation })
                .Keys.Should().BeEquivalentTo("c");
        }
    }
}
