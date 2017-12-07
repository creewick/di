using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IRejectedWordsProvider
    {
        HashSet<string> RejectedWords { get; }
    }
}
