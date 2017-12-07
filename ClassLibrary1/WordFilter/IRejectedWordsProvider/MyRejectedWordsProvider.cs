using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class MyRejectedWordsProvider : IRejectedWordsProvider
    {
        private readonly string filename;

        public MyRejectedWordsProvider(string filename)
        {
            this.filename = filename;
        }

        public HashSet<string> RejectedWords
        {
            get
            {
                if (filename == "")
                    return new HashSet<string>();
                return new HashSet<string>(File.ReadAllLines(filename));
            }
        }
    }
}
