using System;

namespace TagsCloudContainer
{
    public class WordTransformation : IWordTransformation
    {
        public Func<string, string> GetTransformation()
        {
            return s => s.ToLower();
        }
    }
}
