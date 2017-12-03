using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class RandomColoring : IColoringAlgorithm
    {
        public Color GetColor(IEnumerable<Color> colors, string name, int frequency)
        {
            var random = new Random();
            return colors.ElementAt(random.Next(colors.Count() - 1));
        }
    }
}
