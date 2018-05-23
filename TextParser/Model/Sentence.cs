using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IEnumerable<ISentenceItem> AllSentenceItemsAsEnumerable()
        {
            return _sentenceItems.AsEnumerable();
        }

        public ISentenceItem GetSentenceItem(int index)
        {
            return _sentenceItems.ElementAt(index);
        }

        public new string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < _sentenceItems.Count; i++)
            {
                builder.Append(_sentenceItems.ElementAt(i).Chars);
            }
            return builder.ToString();
        }
    }
}
