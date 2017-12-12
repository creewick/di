using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IColoringAlgorithm
    {
        Color GetColor(IEnumerable<Color> colors, string name, int frequency);
    }
}
