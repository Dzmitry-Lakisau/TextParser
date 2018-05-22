namespace TextParser.Model
{
    public struct Symbol
    {
        public string Chars { get; }

        public Symbol(string chars)
        {
            Chars = chars;
        }

        public Symbol(char source)
        {
            Chars = source.ToString();
        }
    }
}
