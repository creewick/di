using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordParser
    {
        IEnumerable<string> GetWords(); 
    }
}
