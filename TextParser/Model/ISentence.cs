using System.Collections.Generic;

namespace TextParser.Model
{
    public interface ISentence
    {
        void AddSentenceItem(ISentenceItem sentenceItem);

        void AddSentenceItemsRange(IEnumerable<ISentenceItem> sentenceItems);

        ISentenceItem GetSentenceItem(int index);
    }
}
