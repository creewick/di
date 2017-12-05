using System;

namespace TagsCloudContainer
{
    public class MyWordTransformation : IWordTransformation
    {
        public Func<string, string> GetTransformation()
        {
            return s => s.ToLower();
        }
    }
}
