using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordTransformation
    {
        Func<string, string> GetTransformation();
    }
}
