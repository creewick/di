using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudBuilder
    {
        Bitmap Build(Dictionary<string, int> frequency);
    }
}
