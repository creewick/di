using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsFilter
    {
        IEnumerable<string> GetFilteredWords(IEnumerable<string> words);
    }
}
