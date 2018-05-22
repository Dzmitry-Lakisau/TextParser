namespace TextParser.Model
{
    public interface ISentenceItemFactory
    {
        ISentenceItem Create(string chars);
    }
}
