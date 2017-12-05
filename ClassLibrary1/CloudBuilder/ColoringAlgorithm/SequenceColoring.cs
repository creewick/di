using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class SequenceColoring : IColoringAlgorithm
    {
        private static int index;

        public Color GetColor(IEnumerable<Color> colors, string name, int frequency)
        {
            return colors.ElementAt(index++ % colors.Count()); 
        }
    }
}
