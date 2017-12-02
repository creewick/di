using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class MyCloudBuilder : ICloudBuilder
    {

        public Bitmap Build(Dictionary<string, int> frequencies, IEnumerable<Color> colors, IEnumerable<string> fontNames, Size size)
        {
            var image = new Bitmap(size.Width, size.Height);
            var g = Graphics.FromImage(image);
            var fontName = fontNames.FirstOrDefault();
            var fontColor = colors.FirstOrDefault();
            var layouter = new CircularCloudLayouter(new Point(size.Width / 2, size.Height / 2));
            foreach (var word in frequencies.Keys)
            {
                var wordSize = g.MeasureString(word, new Font(fontName, frequencies[word]));
                layouter.PutNextWord(word, wordSize, fontName, frequencies[word], fontColor);
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
