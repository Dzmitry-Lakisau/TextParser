namespace TextParser.Model
{
    public class Punctuation: IPunctuation
    {
        public Symbol Value { get; }

        public string Chars//TODO remove
        {
            get { return Value.Chars; }
        }

        public int GetLength()
        {
            return Value.Chars.Length;
        }

        public Punctuation(string chars)
        {
            Value = new Symbol(chars);
        }
    }
}
