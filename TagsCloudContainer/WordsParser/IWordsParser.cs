using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsParser
    {
        IEnumerable<string> GetWords(string filename); 
    }
}
