using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudBuilder
    {
        void Build(Dictionary<string, int> frequency, Graphics g);
    }
}
