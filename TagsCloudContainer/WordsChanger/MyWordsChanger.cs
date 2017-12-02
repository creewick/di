using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class MyWordsChanger : IWordsChanger
    {
        public IEnumerable<string> GetChangedWords(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}
