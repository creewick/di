using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public class WordFilter : IWordFilter
    {
        private readonly IRejectedWordsProvider rejectedWordsProvider;

        public WordFilter(IRejectedWordsProvider rejectedWordsProvider)
        {
            this.rejectedWordsProvider = rejectedWordsProvider;
        }

        public Predicate<string> GetFilter()
        {
            return s => s.Length > 3 && !rejectedWordsProvider.RejectedWords.Contains(s);
        }
    }
}
