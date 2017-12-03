using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class TagsCloudContainer
    {
        public Dictionary<string, int> Words;
        private ICloudBuilder cloudBuilder;

        public TagsCloudContainer(IWordsParser wordsParser, IWordsFilter wordsFilter, IWordsChanger wordsChanger, ICloudBuilder cloudBuilder, string filename)
        {
            var words = wordsParser.GetWords(filename);
            var filteredWords = wordsFilter.GetFilteredWords(words);
            var changedWords = wordsChanger.GetChangedWords(filteredWords);
            Words = GetWordsFrequency(changedWords);
            this.cloudBuilder = cloudBuilder;
        }

        public void SaveAsImage(string filename)
        {
            var image = cloudBuilder.Build(Words);
            image.Save(filename);
        }


        private Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                    result[word] = 1;
                else
                    result[word]++;
            }
            return result;
        }
    }
}
