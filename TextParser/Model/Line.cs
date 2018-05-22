using System.Collections.Generic;
using System.Linq;

namespace TextParser.Model
{
    public class Line: ILine
    {
        private readonly ICollection<Sentence> _sentences;

        private readonly ICollection<ISentenceItem> _sentenceSeparators;

        public Line()
        {
            _sentences = new List<Sentence>();
            _sentenceSeparators = new List<ISentenceItem>();
        }


        public void AddSentence(Sentence sentence)
        {
            _sentences.Add(sentence);
        }

        public void AddSentenceSeparator(ISentenceItem sentenceSeparator)
        {
            _sentenceSeparators.Add(sentenceSeparator);
        }

        public Sentence GetSentence(int index)
        {
            return _sentences.ElementAt(index);
        }

        public ISentenceItem GetSentenceSeparator(int index)
        {
            return _sentenceSeparators.ElementAt(index);
        }
    }
}
