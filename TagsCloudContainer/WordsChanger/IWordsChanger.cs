using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsChanger
    {
        IEnumerable<string> GetChangedWords(IEnumerable<string> words);
    }
}
