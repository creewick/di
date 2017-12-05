using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;
        private readonly Size squareSize = new Size(10, 10);
        private readonly Size rectangleSize = new Size(100, 10);
        private readonly Point center = new Point(500, 500);

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(center, 1, 1);
        }

        [Test]
        public void EmptyState()
        {
            layouter.Center.Should().Be(center);
            layouter.SpiralArgument.Should().Be(0);
            layouter.TotalArea.Should().Be(0);
            layouter.Words.Count.Should().Be(0);
        }

        [Test]
        public void FirstRectangle_PutOnCenter()
        {
            var word = layouter.PutNextWord("", squareSize, "", 1, Color.Red);
            (word.X + word.Width / 2).Should().Be(center.X);
            (word.Y + word.Height / 2).Should().Be(center.Y);
        }

        [Test]
        public void NegativeSize_ArgumentException()
        {
            Action action = () => layouter.PutNextWord("", new Size(-10, -10), "", 1, Color.Red);
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void NewRectangle_AddToList()
        {
            layouter.PutNextWord("", squareSize, "", 1, Color.Red);

            layouter.Words.Count.Should().Be(1);
        }

        [Test]
        public void SecondRectangle_NoIntersect()
        {
            var firstRect = layouter.PutNextWord("", squareSize, "", 1, Color.Red);
            var secondRect = layouter.PutNextWord("", squareSize, "", 1, Color.Red);

            firstRect.IntersectsWith(secondRect).Should().BeFalse();
        }

        [Test]
        public void BigAmount_NoIntersection()
        {
            for (var i = 1; i < 100; i++)
            {
                layouter.PutNextWord("", squareSize, "", 1, Color.Red);
                foreach (var rect in layouter.Words)
                    foreach (var rect2 in layouter.Words)
                        if (rect != rect2)
                            rect.Rectangle.IntersectsWith(rect2.Rectangle).Should().BeFalse();
            }
        }

        [Test]
        public void Squares_LooksLikeCircle()
        {
            const int count = 100;
            
            for (var i = 0; i < count; i++)
                layouter.PutNextWord("", squareSize, "", 1, Color.Red);

            var expectedArea = new CircleFinder(layouter).GetCircleArea();
            var actualArea = squareSize.Height * squareSize.Width * count;

            (actualArea / expectedArea).Should().BeGreaterThan(0.6);
        }

        [Test]
        public void Rectangles_LooksLikeCircle()
        {
            const int count = 200;
            
            for (var i = 1; i < count; i++)
                layouter.PutNextWord("", rectangleSize, "", 1, Color.Red);

            var expectedArea = new CircleFinder(layouter).GetCircleArea();
            var actualArea = rectangleSize.Height * rectangleSize.Width * count;

            (actualArea / expectedArea).Should().BeGreaterThan(0.6);
        }

        [Test]
        public void SquaresAndRectangles_LooksLikeCircle()
        {
            const int count = 100;
            
            var actualArea = 0f;
            for (var i = 0; i < count; i++)
            {
                var rect = layouter.PutNextWord("",
                    i % 2 == 0
                        ? rectangleSize
                        : squareSize,
                    "", 1, Color.Red);
                actualArea += rect.Width * rect.Height;
            }
            var expectedArea = new CircleFinder(layouter).GetCircleArea();

            (actualArea / expectedArea).Should().BeGreaterThan(0.6);
        }

        [Test]
        public void LooksNotLikeCircle_Rearrange()
        {
            var firstRect = layouter.PutNextWord("", squareSize, "", 1, Color.Red); ;
            for (var i = 1; i < 500; i++)
                layouter.PutNextWord("", squareSize, "", 1, Color.Red);
            for (var i = 1; i < 500; i++)
                layouter.PutNextWord("", rectangleSize, "", 1, Color.Red);

            layouter.Words[0].Should().NotBe(firstRect);
        }
    }
}
