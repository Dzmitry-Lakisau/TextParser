namespace TextParser.Model
{
    public interface IPunctuation: ISentenceItem
    {
        Symbol Value { get; }
    }
}
