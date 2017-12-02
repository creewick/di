using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class MyWordsFilter : IWordsFilter
    {
        public IEnumerable<string> GetFilteredWords(IEnumerable<string> words)
        {
            return words.Where(w => w.Length > 3);
        }
    }
}
