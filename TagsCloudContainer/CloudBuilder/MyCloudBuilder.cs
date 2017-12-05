﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class MyCloudBuilder : ICloudBuilder
    { 
        private IColoringAlgorithm coloringAlgorithm;
        private readonly IEnumerable<Color> colors;
        private readonly IEnumerable<string> fontNames;
        private readonly Size size;
        private readonly double step;
        private readonly double factor;

        public MyCloudBuilder(IEnumerable<Color> colors, IColoringAlgorithm coloringAlgorithm, IEnumerable<string> fontNames, Size size, double step, double factor)
        {
            this.coloringAlgorithm = coloringAlgorithm;
            this.colors = colors;
            this.fontNames = fontNames;
            this.size = size;
            this.step = step;
            this.factor = factor;
        }
        public Bitmap Build(Dictionary<string, int> frequency)
        {
            var image = new Bitmap(size.Width, size.Height);
            var g = Graphics.FromImage(image);
            var fontName = fontNames.FirstOrDefault();
            var layouter = new CircularCloudLayouter(new Point(size.Width / 2, size.Height / 2), step, factor);
            foreach (var word in frequency.Keys)
            {
                var wordSize = g.MeasureString(word, new Font(fontName, frequency[word]));
                layouter.PutNextWord(word, wordSize, fontName, frequency[word], 
                    coloringAlgorithm.GetColor(colors, word, frequency[word]));
            }
            var radius = Math.Sqrt(new CircleFinder(layouter).GetSquaredCircleRadius());
            var scale = Math.Min(size.Width, size.Height) / (float)(2 * radius);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            foreach (var word in layouter.Words)
            {
                g.DrawString(word.Value,
                             new Font(word.Font.Name, word.Font.Size * scale),
                             new SolidBrush(word.Color),
                             GetCenterPoint(layouter.Center, word.Rectangle, scale),
                             format);
            }
            return image;
        }

        private PointF GetCenterPoint(Point center, RectangleF rectangle, float scale)
        {
            var rectCenter = new PointF(rectangle.X + rectangle.Width / 2,
                                        rectangle.Y + rectangle.Height / 2);
            var vector = new PointF(rectCenter.X - center.X, rectCenter.Y - center.Y);
            return new PointF(center.X + scale * vector.X, center.Y + scale * vector.Y);
        }
    }
}
