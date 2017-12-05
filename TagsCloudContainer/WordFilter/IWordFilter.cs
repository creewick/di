using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordFilter
    {
        Predicate<string> GetFilter();
    }
}
