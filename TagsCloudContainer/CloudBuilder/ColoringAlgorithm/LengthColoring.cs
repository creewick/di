﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class LengthColoring : IColoringAlgorithm
    {
        public Color GetColor(IEnumerable<Color> colors, string name, int frequency)
        {
            return colors.ElementAt(name.Length % colors.Count());
        }
    }
}
