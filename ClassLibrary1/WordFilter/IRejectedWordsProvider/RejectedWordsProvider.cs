using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class RejectedWordsProvider : IRejectedWordsProvider
    {
        private static Dictionary<string, HashSet<string>> rejectedWordsCache = new Dictionary<string, HashSet<string>>();
        private readonly string filename;

        public RejectedWordsProvider(string filename)
        {
            this.filename = filename;
        }

        public HashSet<string> RejectedWords
        {
            get
            {
                if (filename == "")
                    return new HashSet<string>();
                if (rejectedWordsCache.ContainsKey(filename))
                    return rejectedWordsCache[filename];
                var words = new HashSet<string>(File.ReadAllLines(filename));
                rejectedWordsCache[filename] = words;
                return words;
            }
        }
    }
}
