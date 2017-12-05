using System.Drawing;

namespace TagsCloudContainer
{
    public class Word
    {
        public readonly string Value;
        public readonly Font Font;
        public readonly Color Color;
        public readonly RectangleF Rectangle;

        public Word(string value, string fontName, float fontSize, Color color, RectangleF rectangle)
        {
            Value = value;
            Font = new Font(fontName, fontSize);
            Color = color;
            Rectangle = rectangle;
        }
    }
}
