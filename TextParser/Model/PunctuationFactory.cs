using System.Collections.Generic;

namespace TextParser.Model
{
    public class PunctuationFactory: ISentenceItemFactory
    {
        private readonly IDictionary<string, ISentenceItem> _punctuationItems;
        
        public ISentenceItem Create(string chars)
        {
            return _punctuationItems.ContainsKey(chars) ? _punctuationItems[chars]: null;
        }

        public PunctuationFactory(SeparatorContainer separatorContainer)
        {
            _punctuationItems = new Dictionary<string, ISentenceItem>();
            foreach (var c in separatorContainer.All())
            {
                _punctuationItems.Add(c, new Punctuation(c));
            }
        }
    }
}
