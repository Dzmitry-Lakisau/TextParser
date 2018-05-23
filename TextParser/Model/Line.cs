using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IEnumerable<Sentence> AllSentencesAsEnumerable()
        {
            return _sentences.AsEnumerable();
        }

        public Sentence GetSentence(int index)
        {
            return _sentences.ElementAt(index);
        }

        public ISentenceItem GetSentenceSeparator(int index)
        {
            return _sentenceSeparators.ElementAt(index);
        }

        public new string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i<_sentences.Count; i++)
            {
                builder.Append(_sentences.ElementAt(i).ToString());//.Append(_sentenceSeparators.ElementAt(i).Chars);
            }
            return builder.ToString();
        }
    }
}
