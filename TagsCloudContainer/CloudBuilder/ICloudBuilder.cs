using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudBuilder
    {
        Bitmap Build(Dictionary<string, int> frequencies, IEnumerable<Color> colors, IEnumerable<string> fontNames, Size size);
    }
}
