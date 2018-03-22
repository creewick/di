using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class RandomColoring : IColoringAlgorithm
    {
        private Random random = new Random();

        public Color GetColor(IEnumerable<Color> colors, string name, int frequency)
        {
            return colors.ElementAt(random.Next(colors.Count() - 1));
        }
    }
}
