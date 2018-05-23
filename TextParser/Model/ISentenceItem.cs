namespace TextParser.Model
{
    public interface ISentenceItem
    {
        string Chars { get; }
        int GetLength();
    }
}
