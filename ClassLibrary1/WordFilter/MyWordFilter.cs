using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using ClassLibrary1;

namespace TagsCloudContainer
{
    public class MyWordFilter : IWordFilter
    {
        private readonly IRejectedWordsProvider rejectedWordsProvider;

        public MyWordFilter(IRejectedWordsProvider rejectedWordsProvider)
        {
            this.rejectedWordsProvider = rejectedWordsProvider;
        }

        public Predicate<string> GetFilter()
        {
            return s => s.Length > 3 && !rejectedWordsProvider.RejectedWords.Contains(s);
        }
    }
}
