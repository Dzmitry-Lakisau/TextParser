namespace TextParser.Model
{
    public class SentenceItemFactory: ISentenceItemFactory
    {
        private readonly ISentenceItemFactory _punctuationFactory;
        private readonly ISentenceItemFactory _wordFactory;
        
        public ISentenceItem Create(string chars)
        {
            ISentenceItem result = _punctuationFactory.Create(chars);
            if (result == null)
            {
                result = _wordFactory.Create(chars);
            }
            return result;
        }

        public SentenceItemFactory(ISentenceItemFactory punctuationFactory, ISentenceItemFactory wordFactory)
        {
            _punctuationFactory = punctuationFactory;
            _wordFactory = wordFactory;
        }
    }
}
