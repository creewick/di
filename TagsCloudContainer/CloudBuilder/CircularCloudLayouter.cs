using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    class CircularCloudLayouter
    {
        private readonly double step;
        private readonly double factor;
        public readonly Point Center;
        private bool rearranging;
        public List<Word> Words { get; private set; }
        public List<double> Arguments { get; private set; }
        public float TotalArea => Words.Sum(word => word.Rectangle.Width * word.Rectangle.Height);

        public double SpiralArgument { get; private set; }

        public CircularCloudLayouter(Point center, double step, double factor)
        {
            this.step = step * Math.PI / 180;
            this.factor = factor;
            Center = center;
            Words = new List<Word>();
            Arguments = new List<double>();
        }

        public RectangleF PutNextWord(string text, SizeF wordSize, string fontName, float fontSize, Color fontColor)
        {
            if (wordSize.Width <= 0 || wordSize.Height <= 0)
                throw new ArgumentException();
            RectangleF rect;
            do
            {
                var center = ArchimedeSpiral(SpiralArgument, factor);
                rect = new RectangleF(
                    center.X - wordSize.Width / 2,
                    center.Y - wordSize.Height / 2,
                    wordSize.Width,
                    wordSize.Height
                );
                SpiralArgument += step;
            } while (!IsFreeRectangle(rect));

            Words.Add(new Word(text, fontName, fontSize, fontColor, rect));
            Arguments.Add(SpiralArgument);

            if (!rearranging && !LookingLikeCircle())
                Rearrange();
            return rect;
        }

        private Point ArchimedeSpiral(double argument, double parameter)
        {
            return new Point(
                Center.X + (int)Math.Round(parameter * argument * Math.Cos(argument)),
                Center.Y + (int)Math.Round(parameter * argument * Math.Sin(argument))
            );
        }

        private void Rearrange()
        {
            var sorted = Words.OrderByDescending(x => x.Rectangle.Width * x.Rectangle.Height).ToList();
            var index = 0;
            while (index < Words.Count && Words[index].Equals(sorted[index]))
                index++;
            if (index == Words.Count) return;

            rearranging = true;

            var oldRectangles = Words;
            Words = Words.GetRange(0, index);
            Arguments = Arguments.GetRange(0, index);
            SpiralArgument = Arguments.Count > 0 ? Arguments[Arguments.Count - 1] : 0;

            for (var i = index; i < oldRectangles.Count; i++)
                PutNextWord(sorted[i].Value,
                            sorted[i].Rectangle.Size,
                            sorted[i].Font.Name,
                            sorted[i].Font.Size,
                            sorted[i].Color);

            rearranging = false;
        }

        private bool LookingLikeCircle()
        {
            var circleArea = new CircleFinder(this).GetCircleArea();
            return TotalArea / circleArea > 0.6;
        }

        private bool IsFreeRectangle(RectangleF rect)
        {
            return Words.All(word => !word.Rectangle.IntersectsWith(rect));
        }
    }
}
