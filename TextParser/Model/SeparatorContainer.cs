using System.Collections.Generic;
using System.Linq;

namespace TextParser.Model
{
    public class SeparatorContainer
    {
        private readonly string[] _sentenceSeparators = new[] {"...", "?!", ".", "?", "!"};
        private readonly string[] _wordSeparators = new[]{" - ", ",", ":", ";"};

        public IEnumerable<string> SentenceSeparators()
        {
            return _sentenceSeparators.AsEnumerable();
        }

        public char Space() { return ' '; }

        public IEnumerable<string> WordSeparators()
        {
            return _wordSeparators.AsEnumerable();
        }

        public IEnumerable<string> All()
        {
            return new[]{ Space().ToString()}.Concat(_wordSeparators.Concat(SentenceSeparators()));
        }
    }
}
