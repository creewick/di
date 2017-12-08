using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;


namespace TagsCloudContainer
{
    class TagsCloudContainer_Should
    {
        private IWordParser wordParser;
        private IWordFilter wordFilter;
        private IWordTransformation wordTransformation;
        private ICloudBuilder cloudBuilder;

        [SetUp]
        public void SetUp()
        {
            wordParser = Substitute.For<IWordParser>();
            wordFilter = Substitute.For<IWordFilter>();
            wordTransformation = Substitute.For<IWordTransformation>();
            cloudBuilder = Substitute.For<ICloudBuilder>();
        }

        [Test]
        public void NoFilterNoTransform_SameWordList()
        {
            var words = new[] {"abc", "def"};
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s);
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().Keys.Should().BeEquivalentTo(words);
        }

        [Test]
        public void GetFrequency_GroupBy()
        {
            var words = new[] { "a", "a", "a", "c", "c" };
            var expected = new Dictionary<string, int>{ { "a", 3 }, { "c", 2 } };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s);
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void EmptyWordList()
        {
            var words = new string[0];
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s);
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().Should().BeEmpty();
        }

        [Test]
        public void Filter_FilteredWordList()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => s == "abc");
            wordTransformation.GetTransformation().Returns(s => s);
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().Keys.Should().BeEquivalentTo("abc");
        }

        [Test]
        public void FilterWithEmptyResult()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => false);
            wordTransformation.GetTransformation().Returns(s => s);
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().Keys.Should().BeEmpty();
        }

        [Test]
        public void Transform_TransformedWordList()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => true);
            wordTransformation.GetTransformation().Returns(s => s.ToUpper());
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().Keys.Should().BeEquivalentTo("ABC", "DEF");
        }

        [Test]
        public void FilterAndThenTransform_WordList()
        {
            var words = new[] { "abc", "def" };
            wordParser.GetWords().Returns(words);
            wordFilter.GetFilter().Returns(s => s == "abc");
            wordTransformation.GetTransformation().Returns(s => s.ToUpper());
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter }, wordTransformation, cloudBuilder);

            container.GetFrequency().Keys.Should().BeEquivalentTo("ABC");
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
            var container = new TagsCloudContainer(wordParser, new[] { wordFilter, wordFilter2 }, wordTransformation, cloudBuilder);

            container.GetFrequency().Keys.Should().BeEquivalentTo("c");
        }
    }
}
