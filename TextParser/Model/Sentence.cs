using System.Collections.Generic;
using System.Linq;

namespace TextParser.Model
{
    public class Sentence: ISentence
    {
        private readonly List<ISentenceItem> _sentenceItems;

        public Sentence()
        {
            _sentenceItems = new List<ISentenceItem>();
        }

        public void AddSentenceItem(ISentenceItem sentenceItem)
        {
            _sentenceItems.Add(sentenceItem);
        }

        public void AddSentenceItemsRange(IEnumerable<ISentenceItem> sentenceItems)
        {
            _sentenceItems.AddRange(sentenceItems);
        }

        public ISentenceItem GetSentenceItem(int index)
        {
            return _sentenceItems.ElementAt(index);
        }
    }
}
