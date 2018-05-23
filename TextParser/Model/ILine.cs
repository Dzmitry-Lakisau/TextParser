using System.Collections.Generic;

namespace TextParser.Model
{
    public interface ILine
    {
        void AddSentence(Sentence sentence);

        void AddSentenceSeparator(ISentenceItem sentenceSeparator);

        Sentence GetSentence(int index);

        ISentenceItem GetSentenceSeparator(int index);

        IEnumerable<Sentence> AllSentencesAsEnumerable();
    }
}
