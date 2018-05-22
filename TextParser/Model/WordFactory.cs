namespace TextParser.Model
{
    public class WordFactory: ISentenceItemFactory
    {
        public ISentenceItem Create(string chars)
        {
            return new Word(chars);
        }
    }
}
