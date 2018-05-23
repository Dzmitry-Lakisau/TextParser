using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextParser.Model
{
    public class Word: IWord
    {
        private readonly Symbol[] _symbols;

        public Word(string chars)
        {
            _symbols = chars != null ? chars.Select(x => new Symbol(x)).ToArray(): null;
        }
        
        public Symbol this[int index]
        {
            get { return _symbols[index]; }
        }

        public string Chars
        {
            get 
            {
                StringBuilder sb = new StringBuilder();
                foreach (var s in _symbols)
                {
                    sb.Append(s.Chars);
                }
                return sb.ToString(); 
            }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return _symbols.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _symbols.GetEnumerator();
        }

        public int GetLength()
        {
            return (_symbols != null) ? _symbols.Length : 0;
        }
    }
}
