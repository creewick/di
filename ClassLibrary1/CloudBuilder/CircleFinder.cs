using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class CircleFinder
    {
        private readonly CircularCloudLayouter layouter;

        public CircleFinder(CircularCloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        private double GetSquaredLengthToCenter(float x, float y)
        {
            return Math.Pow(Math.Abs(layouter.Center.X - x), 2) +
                   Math.Pow(Math.Abs(layouter.Center.Y - y), 2);
        }

        public double GetSquaredCircleRadius()
        {
            if (layouter.Words.Count == 0)
                return 0;
            var lastWord = layouter.Words[layouter.Words.Count - 1];
            var firstWord = layouter.Words[0];
            return Math.Max(Math.Max(Math.Max(Math.Max(
                        GetSquaredLengthToCenter(lastWord.Rectangle.Left, lastWord.Rectangle.Top),
                        GetSquaredLengthToCenter(lastWord.Rectangle.Left, lastWord.Rectangle.Bottom)),
                    GetSquaredLengthToCenter(lastWord.Rectangle.Right, lastWord.Rectangle.Top)),
                GetSquaredLengthToCenter(lastWord.Rectangle.Right, lastWord.Rectangle.Bottom)),
                GetSquaredLengthToCenter(firstWord.Rectangle.Left, firstWord.Rectangle.Top));
        }

        /// <summary>
        /// Returns the minimal radius of сircumscribed a circle, which contains every rectangle
        /// </summary>
        /// <param name="lastRectangle">The last added rectangle</param>
        /// <returns></returns>
        public double GetCircleArea()
        {
            return GetSquaredCircleRadius() * Math.PI;
        }
    }
}
